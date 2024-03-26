using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IProcureDatasAppService : IApplicationService 
    {
        Task<PagedResultDto<GetProcureDataForViewDto>> GetAll(GetAllProcureDatasInput input);

        Task<GetProcureDataForViewDto> GetProcureDataForView(int id);

		Task<GetProcureDataForEditOutput> GetProcureDataForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditProcureDataDto input);

		Task Delete(EntityDto input);

		
    }
}