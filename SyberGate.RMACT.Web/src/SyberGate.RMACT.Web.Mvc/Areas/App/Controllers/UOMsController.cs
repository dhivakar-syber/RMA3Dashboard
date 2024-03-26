using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.UOMs;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_UOMs)]
    public class UOMsController : RMACTControllerBase
    {
        private readonly IUOMsAppService _uoMsAppService;

        public UOMsController(IUOMsAppService uoMsAppService)
        {
            _uoMsAppService = uoMsAppService;
        }

        public ActionResult Index()
        {
            var model = new UOMsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_UOMs_Create, AppPermissions.Pages_Administration_UOMs_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetUOMForEditOutput getUOMForEditOutput;

				if (id.HasValue){
					getUOMForEditOutput = await _uoMsAppService.GetUOMForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getUOMForEditOutput = new GetUOMForEditOutput{
						UOM = new CreateOrEditUOMDto()
					};
				}

				var viewModel = new CreateOrEditUOMModalViewModel()
				{
					UOM = getUOMForEditOutput.UOM,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			



    }
}