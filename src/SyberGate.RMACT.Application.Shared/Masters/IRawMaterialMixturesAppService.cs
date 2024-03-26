using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IRawMaterialMixturesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRawMaterialMixtureForViewDto>> GetAll(GetAllRawMaterialMixturesInput input);

		Task<GetRawMaterialMixtureForEditOutput> GetRawMaterialMixtureForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditRawMaterialMixtureDto input);

		Task Delete(EntityDto input);

		
		Task<PagedResultDto<RawMaterialMixtureRMGroupLookupTableDto>> GetAllRMGroupForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RawMaterialMixtureRawMaterialGradeLookupTableDto>> GetAllRawMaterialGradeForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RawMaterialMixtureBuyerLookupTableDto>> GetAllBuyerForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RawMaterialMixtureSupplierLookupTableDto>> GetAllSupplierForLookupTable(GetAllForLookupTableInput input);
		
    }
}