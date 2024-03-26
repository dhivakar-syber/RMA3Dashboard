using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.Ethnicities;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Models;
using SyberGate.RMACT.Models.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Ethnicities)]
    public class EthnicitiesController : RMACTControllerBase
    {
        private readonly IEthnicitiesAppService _ethnicitiesAppService;

        public EthnicitiesController(IEthnicitiesAppService ethnicitiesAppService)
        {
            _ethnicitiesAppService = ethnicitiesAppService;
        }

        public ActionResult Index()
        {
            var model = new EthnicitiesViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_Ethnicities_Create, AppPermissions.Pages_Administration_Ethnicities_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetEthnicityForEditOutput getEthnicityForEditOutput;

				if (id.HasValue){
					getEthnicityForEditOutput = await _ethnicitiesAppService.GetEthnicityForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getEthnicityForEditOutput = new GetEthnicityForEditOutput{
						Ethnicity = new CreateOrEditEthnicityDto()
					};
				}

				var viewModel = new CreateOrEditEthnicityModalViewModel()
				{
					Ethnicity = getEthnicityForEditOutput.Ethnicity,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewEthnicityModal(int id)
        {
			var getEthnicityForViewDto = await _ethnicitiesAppService.GetEthnicityForView(id);

            var model = new EthnicityViewModel()
            {
                Ethnicity = getEthnicityForViewDto.Ethnicity
            };

            return PartialView("_ViewEthnicityModal", model);
        }


    }
}