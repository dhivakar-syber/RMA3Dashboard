using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SyberGate.RMACT.Authorization.Delegation;
using SyberGate.RMACT.Authorization.Users.Delegation;
using SyberGate.RMACT.Web.Areas.App.Models.Layout;
using SyberGate.RMACT.Web.Views;

namespace SyberGate.RMACT.Web.Areas.App.Views.Shared.Components.AppActiveUserDelegationsCombobox
{
    public class AppActiveUserDelegationsComboboxViewComponent : RMACTViewComponent
    {
        private readonly IUserDelegationAppService _userDelegationAppService;
        private readonly IUserDelegationConfiguration _userDelegationConfiguration;

        public AppActiveUserDelegationsComboboxViewComponent(
            IUserDelegationAppService userDelegationAppService, 
            IUserDelegationConfiguration userDelegationConfiguration)
        {
            _userDelegationAppService = userDelegationAppService;
            _userDelegationConfiguration = userDelegationConfiguration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string logoSkin = null, string logoClass = "")
        {
            var activeUserDelegations = await _userDelegationAppService.GetActiveUserDelegations();
            var model = new ActiveUserDelegationsComboboxViewModel
            {
                UserDelegations = activeUserDelegations,
                UserDelegationConfiguration = _userDelegationConfiguration
            };

            return View(model);
        }
    }
}
