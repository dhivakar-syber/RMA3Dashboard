using System.Threading.Tasks;

namespace SyberGate.RMACT.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
