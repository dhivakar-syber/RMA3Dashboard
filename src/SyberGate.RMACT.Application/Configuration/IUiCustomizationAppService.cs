using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.Configuration.Dto;

namespace SyberGate.RMACT.Configuration
{
    public interface IUiCustomizationSettingsAppService : IApplicationService
    {
        Task<List<ThemeSettingsDto>> GetUiManagementSettings();

        Task UpdateUiManagementSettings(ThemeSettingsDto settings);

        Task UpdateDefaultUiManagementSettings(ThemeSettingsDto settings);

        Task UseSystemDefaultSettings();
    }
}
