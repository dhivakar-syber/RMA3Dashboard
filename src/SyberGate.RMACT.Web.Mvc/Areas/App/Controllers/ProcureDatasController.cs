using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.ProcureDatas;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_ProcureDatas)]
    public class ProcureDatasController : RMACTControllerBase
    {
        private readonly IProcureDatasAppService _procureDatasAppService;

        public ProcureDatasController(IProcureDatasAppService procureDatasAppService)
        {
            _procureDatasAppService = procureDatasAppService;
        }

        public ActionResult Index()
        {
            var model = new ProcureDatasViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_ProcureDatas_Create, AppPermissions.Pages_Administration_ProcureDatas_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetProcureDataForEditOutput getProcureDataForEditOutput;

				if (id.HasValue){
					getProcureDataForEditOutput = await _procureDatasAppService.GetProcureDataForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getProcureDataForEditOutput = new GetProcureDataForEditOutput{
						ProcureData = new CreateOrEditProcureDataDto()
					};
				getProcureDataForEditOutput.ProcureData.FromDate = DateTime.Now;
				getProcureDataForEditOutput.ProcureData.ToDate = DateTime.Now;
				getProcureDataForEditOutput.ProcureData.CreationTime = DateTime.Now;
				getProcureDataForEditOutput.ProcureData.DeletionTime = DateTime.Now;
				getProcureDataForEditOutput.ProcureData.LastModificationTime = DateTime.Now;
				}

				var viewModel = new CreateOrEditProcureDataModalViewModel()
				{
					ProcureData = getProcureDataForEditOutput.ProcureData,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewProcureDataModal(int id)
        {
			var getProcureDataForViewDto = await _procureDatasAppService.GetProcureDataForView(id);

            var model = new ProcureDataViewModel()
            {
                ProcureData = getProcureDataForViewDto.ProcureData
            };

            return PartialView("_ViewProcureDataModal", model);
        }


    }
}