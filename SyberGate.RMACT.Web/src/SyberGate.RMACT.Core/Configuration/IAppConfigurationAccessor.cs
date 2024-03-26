using Microsoft.Extensions.Configuration;

namespace SyberGate.RMACT.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
