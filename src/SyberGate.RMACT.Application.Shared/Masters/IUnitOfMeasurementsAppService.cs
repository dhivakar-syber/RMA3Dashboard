using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Masters
{
    public interface IUnitOfMeasurementsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetUnitOfMeasurementForViewDto>> GetAll(GetAllUnitOfMeasurementsInput input);

		Task<GetUnitOfMeasurementForEditOutput> GetUnitOfMeasurementForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditUnitOfMeasurementDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetUnitOfMeasurementsToExcel(GetAllUnitOfMeasurementsForExcelInput input);

		
    }
}