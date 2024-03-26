using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.ApprovalUsers;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using System.Linq;
using Abp.UI;
using Abp.IO.Extensions;
using SyberGate.RMACT.Storage;
using Abp.Runtime.Session;
using Abp.Web.Models;
using Abp.BackgroundJobs;
using SyberGate.RMACT.Masters.Importing;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_ApprovalUsers)]
    public class ApprovalUsersController : RMACTControllerBase
    {
        private readonly IApprovalUsersAppService _approvalUsersAppService;
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;


        public ApprovalUsersController(IApprovalUsersAppService approvalUsersAppService, IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            _approvalUsersAppService = approvalUsersAppService;
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;

        }

        public ActionResult Index()
        {
            var model = new ApprovalUsersViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_ApprovalUsers_Create, AppPermissions.Pages_Administration_ApprovalUsers_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetApprovalUserForEditOutput getApprovalUserForEditOutput;

				if (id.HasValue){
					getApprovalUserForEditOutput = await _approvalUsersAppService.GetApprovalUserForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getApprovalUserForEditOutput = new GetApprovalUserForEditOutput{
						ApprovalUser = new CreateOrEditApprovalUserDto()
					};
				}

				var viewModel = new CreateOrEditApprovalUserModalViewModel()
				{
					ApprovalUser = getApprovalUserForEditOutput.ApprovalUser,					
					      
				};
				
				 

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewApprovalUserModal(int id)
        {
			var getApprovalUserForViewDto = await _approvalUsersAppService.GetApprovalUserForView(id);

            var model = new ApprovalUserViewModel()
            {
                ApprovalUser = getApprovalUserForViewDto.ApprovalUser
            };

            return PartialView("_ViewApprovalUserModal", model);
        }

        [HttpPost]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_ApprovalUsers_Create)]
        public async Task<JsonResult> ImportFromExcel()
        {
            try
            {
                var file = Request.Form.Files.First();

                if (file == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                if (file.Length > 1048576 * 100) //100 MB
                {
                    throw new UserFriendlyException(L("File_SizeLimit_Error"));
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var tenantId = AbpSession.TenantId;
                var fileObject = new BinaryObject(tenantId, fileBytes, $"{DateTime.UtcNow} import from excel file.");

                await BinaryObjectManager.SaveAsync(fileObject);

                await BackgroundJobManager.EnqueueAsync<ImportApprovalUsersToExcelJobs, ImportApprovalUsersFromJobArgs>(new ImportApprovalUsersFromJobArgs
                {
                    TenantId = tenantId,
                    BinaryObjectId = fileObject.Id,
                    User = AbpSession.ToUserIdentifier()
                });


                return Json(new AjaxResponse(new { }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }


    }
}