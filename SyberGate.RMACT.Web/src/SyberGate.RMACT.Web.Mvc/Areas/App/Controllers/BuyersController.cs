using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.Buyers;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Buyers)]
    public class BuyersController : RMACTControllerBase
    {
        private readonly IBuyersAppService _buyersAppService;

        public BuyersController(IBuyersAppService buyersAppService)
        {
            _buyersAppService = buyersAppService;
        }

        public ActionResult Index()
        {
            var model = new BuyersViewModel
			{
				FilterText = ""
			};

            return View(model);
        } 
       

			 [AbpMvcAuthorize(AppPermissions.Pages_Administration_Buyers_Create, AppPermissions.Pages_Administration_Buyers_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id)
			{
				GetBuyerForEditOutput getBuyerForEditOutput;

				if (id.HasValue){
					getBuyerForEditOutput = await _buyersAppService.GetBuyerForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getBuyerForEditOutput = new GetBuyerForEditOutput{
						Buyer = new CreateOrEditBuyerDto()
					};
				}

				var viewModel = new CreateOrEditBuyerModalViewModel()
				{
					Buyer = getBuyerForEditOutput.Buyer,
					UserName = getBuyerForEditOutput.UserName,
					BuyerUserList = await _buyersAppService.GetAllUserForTableDropdown(),                
				};

				return PartialView("_CreateOrEditModal", viewModel);
			}
			



    }
}