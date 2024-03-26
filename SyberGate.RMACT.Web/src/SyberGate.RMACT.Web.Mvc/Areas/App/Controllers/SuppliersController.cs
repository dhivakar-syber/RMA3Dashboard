using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.Suppliers;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Suppliers)]
    public class SuppliersController : RMACTControllerBase
    {
        private readonly ISuppliersAppService _suppliersAppService;

        public SuppliersController(ISuppliersAppService suppliersAppService)
        {
            _suppliersAppService = suppliersAppService;
        }

        public ActionResult Index()
        {
            var model = new SuppliersViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_Suppliers_Create, AppPermissions.Pages_Administration_Suppliers_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetSupplierForEditOutput getSupplierForEditOutput;

				if (id.HasValue){
					getSupplierForEditOutput = await _suppliersAppService.GetSupplierForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getSupplierForEditOutput = new GetSupplierForEditOutput{
						Supplier = new CreateOrEditSupplierDto()
					};
				}

				var viewModel = new CreateOrEditSupplierModalViewModel()
				{
					Supplier = getSupplierForEditOutput.Supplier,                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			

        public async Task<PartialViewResult> ViewSupplierModal(int id)
        {
			var getSupplierForViewDto = await _suppliersAppService.GetSupplierForView(id);

            var model = new SupplierViewModel()
            {
                Supplier = getSupplierForViewDto.Supplier
            };

            return PartialView("_ViewSupplierModal", model);
        }


    }
}