using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.Models
{
    public interface IMedicalSpecialtiesAppService : IApplicationService
    {
        Task<PagedResultDto<GetMedicalSpecialtyForViewDto>> GetAll(GetAllMedicalSpecialtiesInput input);

        Task<GetMedicalSpecialtyForViewDto> GetMedicalSpecialtyForView(int id);

        Task<GetMedicalSpecialtyForEditOutput> GetMedicalSpecialtyForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditMedicalSpecialtyDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetMedicalSpecialtiesToExcel(GetAllMedicalSpecialtiesForExcelInput input);

    }
}