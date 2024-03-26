using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Permissions.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}