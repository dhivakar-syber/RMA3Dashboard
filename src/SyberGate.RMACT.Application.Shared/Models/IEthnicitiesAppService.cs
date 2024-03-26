using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;


namespace SyberGate.RMACT.Models
{
    public interface IEthnicitiesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEthnicityForViewDto>> GetAll(GetAllEthnicitiesInput input);

        Task<GetEthnicityForViewDto> GetEthnicityForView(int id);

		Task<GetEthnicityForEditOutput> GetEthnicityForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditEthnicityDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetEthnicitiesToExcel(GetAllEthnicitiesForExcelInput input);

		
    }
}