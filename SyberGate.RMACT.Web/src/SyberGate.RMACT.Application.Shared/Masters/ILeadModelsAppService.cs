using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface ILeadModelsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLeadModelForViewDto>> GetAll(GetAllLeadModelsInput input);

        Task<GetLeadModelForViewDto> GetLeadModelForView(int id);

		Task<GetLeadModelForEditOutput> GetLeadModelForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditLeadModelDto input);

		Task Delete(EntityDto input);

		
    }
}