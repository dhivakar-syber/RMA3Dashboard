using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IRMGroupsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRMGroupForViewDto>> GetAll(GetAllRMGroupsInput input);

		Task<GetRMGroupForEditOutput> GetRMGroupForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditRMGroupDto input);

		Task Delete(EntityDto input);

		
    }
}