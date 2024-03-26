using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IPlantsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPlantForViewDto>> GetAll(GetAllPlantsInput input);

        Task<GetPlantForViewDto> GetPlantForView(int id);

		Task<GetPlantForEditOutput> GetPlantForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPlantDto input);

		Task Delete(EntityDto input);

		
    }
}