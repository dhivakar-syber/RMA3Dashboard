using Abp.AspNetCore.Mvc.ViewComponents;

namespace SyberGate.RMACT.Web.Views
{
    public abstract class RMACTViewComponent : AbpViewComponent
    {
        protected RMACTViewComponent()
        {
            LocalizationSourceName = RMACTConsts.LocalizationSourceName;
        }
    }
}