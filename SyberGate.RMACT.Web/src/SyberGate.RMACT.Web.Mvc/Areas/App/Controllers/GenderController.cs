using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.Gender;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Models;
using SyberGate.RMACT.Models.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Gender)]
    public class GenderController : RMACTControllerBase
    {
        private readonly IGenderAppService _genderAppService;

        public GenderController(IGenderAppService genderAppService)
        {
            _genderAppService = genderAppService;
        }

        public ActionResult Index()
        {
            var model = new GenderViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Gender_Create, AppPermissions.Pages_Administration_Gender_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetGenderForEditOutput getGenderForEditOutput;

            if (id.HasValue)
            {
                getGenderForEditOutput = await _genderAppService.GetGenderForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getGenderForEditOutput = new GetGenderForEditOutput
                {
                    Gender = new CreateOrEditGenderDto()
                };
            }

            var viewModel = new CreateOrEditGenderModalViewModel()
            {
                Gender = getGenderForEditOutput.Gender,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewGenderModal(int id)
        {
            var getGenderForViewDto = await _genderAppService.GetGenderForView(id);

            var model = new GenderViewModel()
            {
                Gender = getGenderForViewDto.Gender
            };

            return PartialView("_ViewGenderModal", model);
        }

    }
}