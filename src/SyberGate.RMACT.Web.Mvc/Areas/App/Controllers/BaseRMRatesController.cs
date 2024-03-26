using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Web.Areas.App.Models.BaseRMRates;
using SyberGate.RMACT.Web.Controllers;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_BaseRMRates)]
    public class BaseRMRatesController : RMACTControllerBase
    {
        private readonly IBaseRMRatesAppService _baseRMRatesAppService;

        public BaseRMRatesController(IBaseRMRatesAppService baseRMRatesAppService)
        {
            _baseRMRatesAppService = baseRMRatesAppService;
        }

        public ActionResult Index()
        {
            var model = new BaseRMRatesViewModel
			{
				FilterText = ""
			};

            return View(model);
        }

        public async Task<PartialViewResult> BaseRMEditModal(int buyerid, string buyer, int supplierid, string supplier, string period, int rmgroupid)
        {
            var model = new BaseRMHistoryViewModel
            {
                BuyerId = buyerid,
                SupplierId = supplierid,
                Buyer = buyer,
                Supplier = supplier,
                Period = period,
                RMGroupId = rmgroupid
            };

            return PartialView("_BaseRMEditModal", model);
        }

        public async Task<PartialViewResult> BaseRMHistoryModal(int rmid, string rm, int buyerid, string buyer, int supplierid, string supplier)
        {
            var model = new BaseRMHistoryViewModel
            {
                RMGroupId = rmid,
                BuyerId = buyerid,
                SupplierId = supplierid,
                Buyer = buyer,
                Supplier = supplier,
                RMGroup = rm
            };

            return PartialView("_BaseRMHistoryModal", model);
        }

             [AbpMvcAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Create, AppPermissions.Pages_Administration_BaseRMRates_Edit)]
			public async Task<PartialViewResult> CreateOrEditModal(int? id, bool? isrevision)
			{
				GetBaseRMRateForEditOutput getBaseRMRateForEditOutput;

				if (id.HasValue){
					getBaseRMRateForEditOutput = await _baseRMRatesAppService.GetBaseRMRateForEdit(new EntityDto { Id = (int) id });
				}
				else {
					getBaseRMRateForEditOutput = new GetBaseRMRateForEditOutput{
						BaseRMRate = new CreateOrEditBaseRMRateDto()
					};
				}

				var viewModel = new CreateOrEditBaseRMRateModalViewModel()
				{
					BaseRMRate = getBaseRMRateForEditOutput.BaseRMRate,
					RMGroupName = getBaseRMRateForEditOutput.RMGroupName,
					UnitOfMeasurementCode = getBaseRMRateForEditOutput.UnitOfMeasurementCode,
					YearName = getBaseRMRateForEditOutput.YearName,
					BuyerName = getBaseRMRateForEditOutput.BuyerName,
					SupplierName = getBaseRMRateForEditOutput.SupplierName,
                    SettledDate= getBaseRMRateForEditOutput.SettledDate,
					BaseRMRateUnitOfMeasurementList = await _baseRMRatesAppService.GetAllUnitOfMeasurementForTableDropdown(),
					BaseRMRateYearList = await _baseRMRatesAppService.GetAllYearForTableDropdown(),   
                    IsRevision = isrevision ?? false
                   
                };

				return PartialView("_CreateOrEditModal", viewModel);
			}
			


        [AbpMvcAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Create, AppPermissions.Pages_Administration_BaseRMRates_Edit)]
        public PartialViewResult RMGroupLookupTableModal(int? id, string displayName)
        {
            var viewModel = new BaseRMRateRMGroupLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_BaseRMRateRMGroupLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Create, AppPermissions.Pages_Administration_BaseRMRates_Edit)]
        public PartialViewResult BuyerLookupTableModal(int? id, string displayName)
        {
            var viewModel = new BaseRMRateBuyerLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_BaseRMRateBuyerLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_BaseRMRates_Create, AppPermissions.Pages_Administration_BaseRMRates_Edit)]
        public PartialViewResult SupplierLookupTableModal(int? id, string displayName)
        {
            var viewModel = new BaseRMRateSupplierLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_BaseRMRateSupplierLookupTableModal", viewModel);
        }

    }
}