using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IIndexNamesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetIndexNameForViewDto>> GetAll(GetAllIndexNamesInput input);

        Task<GetIndexNameForViewDto> GetIndexNameForView(int id);

		Task<GetIndexNameForEditOutput> GetIndexNameForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditIndexNameDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetIndexNamesToExcel(GetAllIndexNamesForExcelInput input);

		
    }
}