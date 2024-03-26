using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.Plants;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Plants)]
    public class PlantsController : RMACTControllerBase
    {
        private readonly IPlantsAppService _plantsAppService;

        public PlantsController(IPlantsAppService plantsAppService)
        {
            _plantsAppService = plantsAppService;
        }

        public ActionResult Index()
        {
            var model = new PlantsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_Plants_Create, AppPermissions.Pages_Administration_Plants_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetPlantForEditOutput getPlantForEditOutput;

				if (id.HasValue){
					getPlantForEditOutput = await _plantsAppService.GetPlantForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getPlantForEditOutput = new GetPlantForEditOutput{
						Plant = new CreateOrEditPlantDto()
					};
				}

				var viewModel = new CreateOrEditPlantModalViewModel()
				{
					Plant = getPlantForEditOutput.Plant,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewPlantModal(int id)
        {
			var getPlantForViewDto = await _plantsAppService.GetPlantForView(id);

            var model = new PlantViewModel()
            {
                Plant = getPlantForViewDto.Plant
            };

            return PartialView("_ViewPlantModal", model);
        }


    }
}