using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.UnitOfMeasurements;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_UnitOfMeasurements)]
    public class UnitOfMeasurementsController : RMACTControllerBase
    {
        private readonly IUnitOfMeasurementsAppService _unitOfMeasurementsAppService;

        public UnitOfMeasurementsController(IUnitOfMeasurementsAppService unitOfMeasurementsAppService)
        {
            _unitOfMeasurementsAppService = unitOfMeasurementsAppService;
        }

        public ActionResult Index()
        {
            var model = new UnitOfMeasurementsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_UnitOfMeasurements_Create, AppPermissions.Pages_Administration_UnitOfMeasurements_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetUnitOfMeasurementForEditOutput getUnitOfMeasurementForEditOutput;

				if (id.HasValue){
					getUnitOfMeasurementForEditOutput = await _unitOfMeasurementsAppService.GetUnitOfMeasurementForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getUnitOfMeasurementForEditOutput = new GetUnitOfMeasurementForEditOutput{
						UnitOfMeasurement = new CreateOrEditUnitOfMeasurementDto()
					};
				}

				var viewModel = new CreateOrEditUnitOfMeasurementModalViewModel()
				{
					UnitOfMeasurement = getUnitOfMeasurementForEditOutput.UnitOfMeasurement,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			



    }
}