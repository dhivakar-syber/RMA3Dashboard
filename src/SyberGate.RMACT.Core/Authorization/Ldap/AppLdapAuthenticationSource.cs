using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.MultiTenancy;

namespace SyberGate.RMACT.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}