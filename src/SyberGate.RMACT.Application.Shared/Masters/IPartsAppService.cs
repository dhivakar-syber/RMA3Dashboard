using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IPartsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPartForViewDto>> GetAll(GetAllPartsInput input);

		Task<GetPartForEditOutput> GetPartForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPartDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPartsToExcel(GetAllPartsForExcelInput input);

		
		Task<PagedResultDto<PartSupplierLookupTableDto>> GetAllSupplierForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PartBuyerLookupTableDto>> GetAllBuyerForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PartRMGroupLookupTableDto>> GetAllRMGroupForLookupTable(GetAllForLookupTableInput input);
		
    }
}