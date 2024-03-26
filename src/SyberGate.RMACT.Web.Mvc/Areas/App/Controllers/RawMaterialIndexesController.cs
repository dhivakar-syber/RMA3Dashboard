using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.RawMaterialIndexes;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes)]
    public class RawMaterialIndexesController : RMACTControllerBase
    {
        private readonly IRawMaterialIndexesAppService _rawMaterialIndexesAppService;

        public RawMaterialIndexesController(IRawMaterialIndexesAppService rawMaterialIndexesAppService)
        {
            _rawMaterialIndexesAppService = rawMaterialIndexesAppService;
        }

        public ActionResult Index()
        {
            var model = new RawMaterialIndexesViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes_Create, AppPermissions.Pages_Administration_RawMaterialIndexes_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetRawMaterialIndexForEditOutput getRawMaterialIndexForEditOutput;

				if (id.HasValue){
					getRawMaterialIndexForEditOutput = await _rawMaterialIndexesAppService.GetRawMaterialIndexForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getRawMaterialIndexForEditOutput = new GetRawMaterialIndexForEditOutput{
						RawMaterialIndex = new CreateOrEditRawMaterialIndexDto()
					};
				}

				var viewModel = new CreateOrEditRawMaterialIndexModalViewModel()
				{
					RawMaterialIndex = getRawMaterialIndexForEditOutput.RawMaterialIndex,
					IndexNameName = getRawMaterialIndexForEditOutput.IndexNameName,
					YearName = getRawMaterialIndexForEditOutput.YearName,
					RawMaterialGradeName = getRawMaterialIndexForEditOutput.RawMaterialGradeName,
					RawMaterialIndexIndexNameList = await _rawMaterialIndexesAppService.GetAllIndexNameForTableDropdown(),
					RawMaterialIndexYearList = await _rawMaterialIndexesAppService.GetAllYearForTableDropdown(),                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewRawMaterialIndexModal(int id)
        {
			var getRawMaterialIndexForViewDto = await _rawMaterialIndexesAppService.GetRawMaterialIndexForView(id);

            var model = new RawMaterialIndexViewModel()
            {
                RawMaterialIndex = getRawMaterialIndexForViewDto.RawMaterialIndex
                , IndexNameName = getRawMaterialIndexForViewDto.IndexNameName 

                , YearName = getRawMaterialIndexForViewDto.YearName 

                , RawMaterialGradeName = getRawMaterialIndexForViewDto.RawMaterialGradeName 

            };

            return PartialView("_ViewRawMaterialIndexModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_RawMaterialIndexes_Create, AppPermissions.Pages_Administration_RawMaterialIndexes_Edit)]
        public PartialViewResult RawMaterialGradeLookupTableModal(int? id, string displayName)
        {
            var viewModel = new RawMaterialIndexRawMaterialGradeLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_RawMaterialIndexRawMaterialGradeLookupTableModal", viewModel);
        }

    }
}