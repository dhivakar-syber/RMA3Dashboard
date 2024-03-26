using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IA3DocumentsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetA3DocumentForViewDto>> GetAll(GetAllA3DocumentsInput input);

		Task<GetA3DocumentForEditOutput> GetA3DocumentForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditA3DocumentDto input);

		Task Delete(EntityDto input);

        Task uploadexcel(SupportAttachmentsDto input);


    }
}