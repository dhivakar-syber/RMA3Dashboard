using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IRawMaterialGradesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRawMaterialGradeForViewDto>> GetAll(GetAllRawMaterialGradesInput input);

		Task<GetRawMaterialGradeForEditOutput> GetRawMaterialGradeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditRawMaterialGradeDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetRawMaterialGradesToExcel(GetAllRawMaterialGradesForExcelInput input);

		
		Task<PagedResultDto<RawMaterialGradeRawMaterialGradeLookupTableDto>> GetAllRawMaterialGradeForLookupTable(GetAllForLookupTableInput input);
		
    }
}