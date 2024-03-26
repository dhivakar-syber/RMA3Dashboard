using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IUOMsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetUOMForViewDto>> GetAll(GetAllUOMsInput input);

		Task<GetUOMForEditOutput> GetUOMForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditUOMDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetUOMsToExcel(GetAllUOMsForExcelInput input);

		
    }
}