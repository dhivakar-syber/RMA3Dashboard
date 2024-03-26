using Abp.AspNetCore.Mvc.ViewComponents;

namespace SyberGate.RMACT.Web.Public.Views
{
    public abstract class RMACTViewComponent : AbpViewComponent
    {
        protected RMACTViewComponent()
        {
            LocalizationSourceName = RMACTConsts.LocalizationSourceName;
        }
    }
}