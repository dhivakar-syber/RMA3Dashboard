using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.MedicinalTypes;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Models;
using SyberGate.RMACT.Models.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_MedicinalTypes)]
    public class MedicinalTypesController : RMACTControllerBase
    {
        private readonly IMedicinalTypesAppService _medicinalTypesAppService;

        public MedicinalTypesController(IMedicinalTypesAppService medicinalTypesAppService)
        {
            _medicinalTypesAppService = medicinalTypesAppService;
        }

        public ActionResult Index()
        {
            var model = new MedicinalTypesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_MedicinalTypes_Create, AppPermissions.Pages_Administration_MedicinalTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetMedicinalTypeForEditOutput getMedicinalTypeForEditOutput;

            if (id.HasValue)
            {
                getMedicinalTypeForEditOutput = await _medicinalTypesAppService.GetMedicinalTypeForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getMedicinalTypeForEditOutput = new GetMedicinalTypeForEditOutput
                {
                    MedicinalType = new CreateOrEditMedicinalTypeDto()
                };
            }

            var viewModel = new CreateOrEditMedicinalTypeModalViewModel()
            {
                MedicinalType = getMedicinalTypeForEditOutput.MedicinalType,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewMedicinalTypeModal(int id)
        {
            var getMedicinalTypeForViewDto = await _medicinalTypesAppService.GetMedicinalTypeForView(id);

            var model = new MedicinalTypeViewModel()
            {
                MedicinalType = getMedicinalTypeForViewDto.MedicinalType
            };

            return PartialView("_ViewMedicinalTypeModal", model);
        }

    }
}