using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.RawMaterialMixtures;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures)]
    public class RawMaterialMixturesController : RMACTControllerBase
    {
        private readonly IRawMaterialMixturesAppService _rawMaterialMixturesAppService;

        public RawMaterialMixturesController(IRawMaterialMixturesAppService rawMaterialMixturesAppService)
        {
            _rawMaterialMixturesAppService = rawMaterialMixturesAppService;
        }

        public ActionResult Index()
        {
            var model = new RawMaterialMixturesViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Create, AppPermissions.Pages_Administration_RawMaterialMixtures_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetRawMaterialMixtureForEditOutput getRawMaterialMixtureForEditOutput;

				if (id.HasValue){
					getRawMaterialMixtureForEditOutput = await _rawMaterialMixturesAppService.GetRawMaterialMixtureForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getRawMaterialMixtureForEditOutput = new GetRawMaterialMixtureForEditOutput{
						RawMaterialMixture = new CreateOrEditRawMaterialMixtureDto()
					};
				}

				var viewModel = new CreateOrEditRawMaterialMixtureModalViewModel()
				{
					RawMaterialMixture = getRawMaterialMixtureForEditOutput.RawMaterialMixture,
					RMGroupName = getRawMaterialMixtureForEditOutput.RMGroupName,
					RawMaterialGradeName = getRawMaterialMixtureForEditOutput.RawMaterialGradeName,
					BuyerName = getRawMaterialMixtureForEditOutput.BuyerName,
					SupplierName = getRawMaterialMixtureForEditOutput.SupplierName,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			


        [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Create, AppPermissions.Pages_Administration_RawMaterialMixtures_Edit)]
        public PartialViewResult RMGroupLookupTableModal(int? id, string displayName)
        {
            var viewModel = new RawMaterialMixtureRMGroupLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_RawMaterialMixtureRMGroupLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Create, AppPermissions.Pages_Administration_RawMaterialMixtures_Edit)]
        public PartialViewResult RawMaterialGradeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new RawMaterialMixtureRawMaterialGradeLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_RawMaterialMixtureRawMaterialGradeLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Create, AppPermissions.Pages_Administration_RawMaterialMixtures_Edit)]
        public PartialViewResult BuyerLookupTableModal(int? id, string displayName)
        {
            var viewModel = new RawMaterialMixtureBuyerLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_RawMaterialMixtureBuyerLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialMixtures_Create, AppPermissions.Pages_Administration_RawMaterialMixtures_Edit)]
        public PartialViewResult SupplierLookupTableModal(int? id, string displayName)
        {
            var viewModel = new RawMaterialMixtureSupplierLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_RawMaterialMixtureSupplierLookupTableModal", viewModel);
        }

    }
}