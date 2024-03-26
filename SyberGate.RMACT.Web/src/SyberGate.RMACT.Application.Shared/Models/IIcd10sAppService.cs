using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Models
{
    public interface IIcd10sAppService : IApplicationService
    {
        Task<PagedResultDto<GetIcd10ForViewDto>> GetAll(GetAllIcd10sInput input);

        Task<GetIcd10ForViewDto> GetIcd10ForView(int id);

        Task<GetIcd10ForEditOutput> GetIcd10ForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditIcd10Dto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetIcd10sToExcel(GetAllIcd10sForExcelInput input);

    }
}