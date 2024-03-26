using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;
using System.Collections.Generic;


namespace SyberGate.RMACT.Masters
{
    public interface IPartModelMatrixesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPartModelMatrixForViewDto>> GetAll(GetAllPartModelMatrixesInput input);

        Task<GetPartModelMatrixForViewDto> GetPartModelMatrixForView(int id);

		Task<GetPartModelMatrixForEditOutput> GetPartModelMatrixForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPartModelMatrixDto input);

		Task Delete(EntityDto input);

		
		Task<List<PartModelMatrixLeadModelLookupTableDto>> GetAllLeadModelForTableDropdown();
		
    }
}