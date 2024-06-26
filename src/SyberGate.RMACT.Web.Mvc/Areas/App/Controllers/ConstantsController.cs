using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.Constants;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Models;
using SyberGate.RMACT.Models.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Constants)]
    public class ConstantsController : RMACTControllerBase
    {
        private readonly IConstantsAppService _constantsAppService;

        public ConstantsController(IConstantsAppService constantsAppService)
        {
            _constantsAppService = constantsAppService;
        }

        public ActionResult Index()
        {
            var model = new ConstantsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Constants_Create, AppPermissions.Pages_Constants_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetConstantForEditOutput getConstantForEditOutput;

            if (id.HasValue)
            {
                getConstantForEditOutput = await _constantsAppService.GetConstantForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getConstantForEditOutput = new GetConstantForEditOutput
                {
                    Constant = new CreateOrEditConstantDto()
                };
            }

            var viewModel = new CreateOrEditConstantModalViewModel()
            {
                Constant = getConstantForEditOutput.Constant,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewConstantModal(int id)
        {
            var getConstantForViewDto = await _constantsAppService.GetConstantForView(id);

            var model = new ConstantViewModel()
            {
                Constant = getConstantForViewDto.Constant
            };

            return PartialView("_ViewConstantModal", model);
        }

    }
}