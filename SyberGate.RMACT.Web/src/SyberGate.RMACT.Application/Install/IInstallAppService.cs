using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.Install.Dto;

namespace SyberGate.RMACT.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}