using System;
using SyberGate.RMACT.Core;
using SyberGate.RMACT.Core.Dependency;
using SyberGate.RMACT.Services.Permission;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SyberGate.RMACT.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class HasPermissionExtension : IMarkupExtension
    {
        public string Text { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return false;
            }

            var permissionService = DependencyResolver.Resolve<IPermissionService>();
            return permissionService.HasPermission(Text);
        }
    }
}