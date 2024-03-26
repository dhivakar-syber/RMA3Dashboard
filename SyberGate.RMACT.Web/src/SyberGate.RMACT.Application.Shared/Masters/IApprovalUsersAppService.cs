using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IApprovalUsersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetApprovalUserForViewDto>> GetAll(GetAllApprovalUsersInput input);

        Task<GetApprovalUserForViewDto> GetApprovalUserForView(int id);

		Task<GetApprovalUserForEditOutput> GetApprovalUserForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditApprovalUserDto input);

		Task Delete(EntityDto input);

		
		
		
    }
}