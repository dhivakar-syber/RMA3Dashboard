using System.Collections.Generic;
using Abp.Application.Services.Dto;
using SyberGate.RMACT.Authorization.Permissions.Dto;
using SyberGate.RMACT.Web.Areas.App.Models.Common;

namespace SyberGate.RMACT.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}