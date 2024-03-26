using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Permissions.Dto;

namespace SyberGate.RMACT.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}