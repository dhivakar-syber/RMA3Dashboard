using Abp.Authorization;
using SyberGate.RMACT.Authorization.Roles;
using SyberGate.RMACT.Authorization.Users;

namespace SyberGate.RMACT.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
