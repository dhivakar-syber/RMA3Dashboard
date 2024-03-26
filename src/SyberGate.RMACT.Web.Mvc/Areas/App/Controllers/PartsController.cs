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
using SyberGate.RMACT.Web.Areas.App.Models.Parts;
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
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Parts)]
    public class PartsController : RMACTControllerBase
    {
        private readonly IPartsAppService _partsAppService;
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;

        public PartsController(IPartsAppService partsAppService, IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            _partsAppService = partsAppService;
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
        }

        public ActionResult Index()
        {
            var model = new PartsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_Parts_Create, AppPermissions.Pages_Administration_Parts_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetPartForEditOutput getPartForEditOutput;

				if (id.HasValue){
					getPartForEditOutput = await _partsAppService.GetPartForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getPartForEditOutput = new GetPartForEditOutput{
						Part = new CreateOrEditPartDto()
					};
				}

				var viewModel = new CreateOrEditPartModalViewModel()
				{
					Part = getPartForEditOutput.Part,
					SupplierName = getPartForEditOutput.SupplierName,
					BuyerName = getPartForEditOutput.BuyerName,
					RMGroupName = getPartForEditOutput.RMGroupName,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			


        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Parts_Create, AppPermissions.Pages_Administration_Parts_Edit)]
        public PartialViewResult SupplierLookupTableModal(int? id, string displayName)
        {
            var viewModel = new PartSupplierLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_PartSupplierLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Parts_Create, AppPermissions.Pages_Administration_Parts_Edit)]
        public PartialViewResult BuyerLookupTableModal(int? id, string displayName)
        {
            var viewModel = new PartBuyerLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_PartBuyerLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Parts_Create, AppPermissions.Pages_Administration_Parts_Edit)]
        public PartialViewResult RMGroupLookupTableModal(int? id, string displayName)
        {
            var viewModel = new PartRMGroupLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_PartRMGroupLookupTableModal", viewModel);
        }

        [HttpPost]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Parts_Edit)]
        public async Task<JsonResult> ImportPartMasterFromExcel()
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

                await BackgroundJobManager.EnqueueAsync<ImportPartsToExcelJob, ImportPartsFromExcelJobArgs>(new ImportPartsFromExcelJobArgs
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

        [HttpPost]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Parts_Edit)]
        public async Task<JsonResult> ImportGlobusDataFromExcel()
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

                await BackgroundJobManager.EnqueueAsync<ImportGlobusDatasToExcelJob, ImportGlobusDatasFromExcelJobArgs>(new ImportGlobusDatasFromExcelJobArgs
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