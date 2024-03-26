using System.Collections.Generic;
using SyberGate.RMACT.Authorization.Permissions.Dto;

namespace SyberGate.RMACT.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}