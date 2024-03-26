using System.Reflection;
using System.Resources;
using Abp.Dependency;

namespace SyberGate.RMACT.Localization
{
    public static class LocalTranslationHelper
    {
        private const string ResourceId = "SyberGate.RMACT.Localization.Resources.LocalTranslation";

        public static string Localize(string key)
        {
            return GetValue(key) ?? key;
        } 
        
        private static string GetValue(string key)
        {
            var locale = IocManager.Instance.Resolve<ILocale>();
            var cultureInfo = locale.GetCurrentCultureInfo();
            var resourceManager = new ResourceManager(ResourceId, typeof(LocalTranslationHelper).GetTypeInfo().Assembly);
            return resourceManager.GetString(key, cultureInfo);
        }
    }
}