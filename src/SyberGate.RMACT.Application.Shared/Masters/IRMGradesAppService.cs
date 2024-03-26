using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IRMGradesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRMGradeForViewDto>> GetAll(GetAllRMGradesInput input);

		Task<GetRMGradeForEditOutput> GetRMGradeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditRMGradeDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetRMGradesToExcel(GetAllRMGradesForExcelInput input);

		
    }
}