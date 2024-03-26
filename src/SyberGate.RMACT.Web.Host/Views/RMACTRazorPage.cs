using Abp.AspNetCore.Mvc.Views;

namespace SyberGate.RMACT.Web.Views
{
    public abstract class RMACTRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected RMACTRazorPage()
        {
            LocalizationSourceName = RMACTConsts.LocalizationSourceName;
        }
    }
}
