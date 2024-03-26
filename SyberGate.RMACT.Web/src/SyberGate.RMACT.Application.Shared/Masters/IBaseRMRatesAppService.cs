using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using System.Collections.Generic;
using System.Collections.Generic;


namespace SyberGate.RMACT.Masters
{
    public interface IBaseRMRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetBaseRMRateForViewDto>> GetAll(GetAllBaseRMRatesInput input);

		Task<GetBaseRMRateForEditOutput> GetBaseRMRateForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditBaseRMRateDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetBaseRMRatesToExcel(GetAllBaseRMRatesForExcelInput input);

		
		Task<PagedResultDto<BaseRMRateRMGroupLookupTableDto>> GetAllRMGroupForLookupTable(GetAllForLookupTableInput input);
		
		Task<List<BaseRMRateUnitOfMeasurementLookupTableDto>> GetAllUnitOfMeasurementForTableDropdown();
		
		Task<List<BaseRMRateYearLookupTableDto>> GetAllYearForTableDropdown();
		
		Task<PagedResultDto<BaseRMRateBuyerLookupTableDto>> GetAllBuyerForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<BaseRMRateSupplierLookupTableDto>> GetAllSupplierForLookupTable(GetAllForLookupTableInput input);
		
    }
}