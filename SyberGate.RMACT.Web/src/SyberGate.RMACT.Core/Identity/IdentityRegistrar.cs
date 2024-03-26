using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SyberGate.RMACT.Authentication.TwoFactor.Google;
using SyberGate.RMACT.Authorization;
using SyberGate.RMACT.Authorization.Roles;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Editions;
using SyberGate.RMACT.MultiTenancy;

namespace SyberGate.RMACT.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>(options =>
                {
                    options.Tokens.ProviderMap[GoogleAuthenticatorProvider.Name] = new TokenProviderDescriptor(typeof(GoogleAuthenticatorProvider));
                })
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
