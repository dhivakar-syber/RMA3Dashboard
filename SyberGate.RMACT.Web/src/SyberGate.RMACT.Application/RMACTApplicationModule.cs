using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SyberGate.RMACT.Authorization;

namespace SyberGate.RMACT
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(RMACTApplicationSharedModule),
        typeof(RMACTCoreModule)
        )]
    public class RMACTApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RMACTApplicationModule).GetAssembly());
        }
    }
}