using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.RMGroups;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_RMGroups)]
    public class RMGroupsController : RMACTControllerBase
    {
        private readonly IRMGroupsAppService _rmGroupsAppService;

        public RMGroupsController(IRMGroupsAppService rmGroupsAppService)
        {
            _rmGroupsAppService = rmGroupsAppService;
        }

        public ActionResult Index()
        {
            var model = new RMGroupsViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_RMGroups_Create, AppPermissions.Pages_Administration_RMGroups_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetRMGroupForEditOutput getRMGroupForEditOutput;

				if (id.HasValue){
					getRMGroupForEditOutput = await _rmGroupsAppService.GetRMGroupForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getRMGroupForEditOutput = new GetRMGroupForEditOutput{
						RMGroup = new CreateOrEditRMGroupDto()
					};
				}

				var viewModel = new CreateOrEditRMGroupModalViewModel()
				{
					RMGroup = getRMGroupForEditOutput.RMGroup,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			



    }
}