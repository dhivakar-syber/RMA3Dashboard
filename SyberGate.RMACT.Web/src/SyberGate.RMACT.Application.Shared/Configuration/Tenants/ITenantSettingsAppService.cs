using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.Configuration.Tenants.Dto;

namespace SyberGate.RMACT.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
