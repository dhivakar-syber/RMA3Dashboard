using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.UI;
using Abp.IO.Extensions;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Abp.Runtime.Session;
using SyberGate.RMACT.Masters.Importing;
using SyberGate.RMACT.Web.Areas.App.Models.LeadModels;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Storage;
using Abp.Application.Services.Dto;
using Abp.BackgroundJobs;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_LeadModels)]
    public class LeadModelsController : RMACTControllerBase
    {
        private readonly ILeadModelsAppService _leadModelsAppService;
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;

        public LeadModelsController(ILeadModelsAppService leadModelsAppService, IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            _leadModelsAppService = leadModelsAppService;
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
        }

        public ActionResult Index()
        {
            var model = new LeadModelsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_LeadModels_Create, AppPermissions.Pages_Administration_LeadModels_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetLeadModelForEditOutput getLeadModelForEditOutput;

				if (id.HasValue){
					getLeadModelForEditOutput = await _leadModelsAppService.GetLeadModelForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getLeadModelForEditOutput = new GetLeadModelForEditOutput{
						LeadModel = new CreateOrEditLeadModelDto()
					};
				}

				var viewModel = new CreateOrEditLeadModelModalViewModel()
				{
					LeadModel = getLeadModelForEditOutput.LeadModel,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewLeadModelModal(int id)
        {
			var getLeadModelForViewDto = await _leadModelsAppService.GetLeadModelForView(id);

            var model = new LeadModelViewModel()
            {
                LeadModel = getLeadModelForViewDto.LeadModel
            };

            return PartialView("_ViewLeadModelModal", model);
        }

        [HttpPost]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_LeadModels_Create)]
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

                await BackgroundJobManager.EnqueueAsync<ImportLeadModelsToExcelJob, ImportLeadModelsFromExcelJobArgs>(new ImportLeadModelsFromExcelJobArgs
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