using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Models
{
    public interface IGenderAppService : IApplicationService
    {
        Task<PagedResultDto<GetGenderForViewDto>> GetAll(GetAllGenderInput input);

        Task<GetGenderForViewDto> GetGenderForView(int id);

        Task<GetGenderForEditOutput> GetGenderForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditGenderDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetGenderToExcel(GetAllGenderForExcelInput input);

    }
}