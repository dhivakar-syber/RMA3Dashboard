using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IYearsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetYearForViewDto>> GetAll(GetAllYearsInput input);

		Task<GetYearForEditOutput> GetYearForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditYearDto input);

		Task Delete(EntityDto input);

		
    }
}