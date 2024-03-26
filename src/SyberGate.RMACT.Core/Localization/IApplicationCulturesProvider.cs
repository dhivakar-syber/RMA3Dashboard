using System.Globalization;

namespace SyberGate.RMACT.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}