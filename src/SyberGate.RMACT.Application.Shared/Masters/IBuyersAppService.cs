using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using System.Collections.Generic;


namespace SyberGate.RMACT.Masters
{
    public interface IBuyersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetBuyerForViewDto>> GetAll(GetAllBuyersInput input);

		Task<GetBuyerForEditOutput> GetBuyerForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditBuyerDto input);

		Task Delete(EntityDto input);

		
		Task<List<BuyerUserLookupTableDto>> GetAllUserForTableDropdown();
		
    }
}