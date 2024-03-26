using Abp.Application.Services;
using SyberGate.RMACT.Dto;
using SyberGate.RMACT.Logging.Dto;

namespace SyberGate.RMACT.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
