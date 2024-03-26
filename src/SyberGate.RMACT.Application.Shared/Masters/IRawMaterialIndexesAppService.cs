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
    public interface IRawMaterialIndexesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRawMaterialIndexForViewDto>> GetAll(GetAllRawMaterialIndexesInput input);

        Task<GetRawMaterialIndexForViewDto> GetRawMaterialIndexForView(int id);

		Task<GetRawMaterialIndexForEditOutput> GetRawMaterialIndexForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditRawMaterialIndexDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetRawMaterialIndexesToExcel(GetAllRawMaterialIndexesForExcelInput input);

		
		Task<List<RawMaterialIndexIndexNameLookupTableDto>> GetAllIndexNameForTableDropdown();
		
		Task<List<RawMaterialIndexYearLookupTableDto>> GetAllYearForTableDropdown();
		
		Task<PagedResultDto<RawMaterialIndexRawMaterialGradeLookupTableDto>> GetAllRawMaterialGradeForLookupTable(GetAllForLookupTableInput input);
		
    }
}