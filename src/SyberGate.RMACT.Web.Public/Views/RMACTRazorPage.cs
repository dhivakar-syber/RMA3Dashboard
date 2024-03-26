using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace SyberGate.RMACT.Web.Public.Views
{
    public abstract class RMACTRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected RMACTRazorPage()
        {
            LocalizationSourceName = RMACTConsts.LocalizationSourceName;
        }
    }
}
