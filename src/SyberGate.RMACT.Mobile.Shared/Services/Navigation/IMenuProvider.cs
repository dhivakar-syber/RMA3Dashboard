using System.Collections.Generic;
using MvvmHelpers;
using SyberGate.RMACT.Models.NavigationMenu;

namespace SyberGate.RMACT.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}