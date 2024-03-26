using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.Years;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Years)]
    public class YearsController : RMACTControllerBase
    {
        private readonly IYearsAppService _yearsAppService;

        public YearsController(IYearsAppService yearsAppService)
        {
            _yearsAppService = yearsAppService;
        }

        public ActionResult Index()
        {
            var model = new YearsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_Years_Create, AppPermissions.Pages_Administration_Years_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetYearForEditOutput getYearForEditOutput;

				if (id.HasValue){
					getYearForEditOutput = await _yearsAppService.GetYearForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getYearForEditOutput = new GetYearForEditOutput{
						Year = new CreateOrEditYearDto()
					};
				}

				var viewModel = new CreateOrEditYearModalViewModel()
				{
					Year = getYearForEditOutput.Year,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			



    }
}