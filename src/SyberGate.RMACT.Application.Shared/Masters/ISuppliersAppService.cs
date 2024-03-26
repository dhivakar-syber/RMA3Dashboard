using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface ISuppliersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetSupplierForViewDto>> GetAll(GetAllSuppliersInput input);

        Task<GetSupplierForViewDto> GetSupplierForView(int id);

		Task<GetSupplierForEditOutput> GetSupplierForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditSupplierDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetSuppliersToExcel(GetAllSuppliersForExcelInput input);

		
    }
}