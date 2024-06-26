using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.Configuration.Host.Dto;

namespace SyberGate.RMACT.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
