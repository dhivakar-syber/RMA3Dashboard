using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.PartModelMatrixes;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using SyberGate.RMACT.Masters.Importing;
using SyberGate.RMACT.Storage;
using System.Linq;
using Abp.IO.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes)]
    public class PartModelMatrixesController : RMACTControllerBase
    {
        private readonly IPartModelMatrixesAppService _partModelMatrixesAppService;
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;

        public PartModelMatrixesController(IPartModelMatrixesAppService partModelMatrixesAppService, IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            _partModelMatrixesAppService = partModelMatrixesAppService;
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
        }

        public ActionResult Index()
        {
            var model = new PartModelMatrixesViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes, AppPermissions.Pages_Administration_PartModelMatrixes_Create, AppPermissions.Pages_Administration_PartModelMatrixes_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetPartModelMatrixForEditOutput getPartModelMatrixForEditOutput;

				if (id.HasValue){
					getPartModelMatrixForEditOutput = await _partModelMatrixesAppService.GetPartModelMatrixForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getPartModelMatrixForEditOutput = new GetPartModelMatrixForEditOutput{
						PartModelMatrix = new CreateOrEditPartModelMatrixDto()
					};
				}

				var viewModel = new CreateOrEditPartModelMatrixModalViewModel()
				{
					PartModelMatrix = getPartModelMatrixForEditOutput.PartModelMatrix,
					LeadModelName = getPartModelMatrixForEditOutput.LeadModelName,
					PartModelMatrixLeadModelList = await _partModelMatrixesAppService.GetAllLeadModelForTableDropdown(),                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewPartModelMatrixModal(int id)
        {
			var getPartModelMatrixForViewDto = await _partModelMatrixesAppService.GetPartModelMatrixForView(id);

            var model = new PartModelMatrixViewModel()
            {
                PartModelMatrix = getPartModelMatrixForViewDto.PartModelMatrix
                , LeadModelName = getPartModelMatrixForViewDto.LeadModelName 

            };

            return PartialView("_ViewPartModelMatrixModal", model);
        }
        [HttpPost]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_PartModelMatrixes_Create)]
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

                await BackgroundJobManager.EnqueueAsync<ImportPartModelMatrixToExcelJob, ImportPartModelMatrixFromJobArgs>(new ImportPartModelMatrixFromJobArgs
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