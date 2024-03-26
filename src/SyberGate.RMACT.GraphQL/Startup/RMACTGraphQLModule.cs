using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SyberGate.RMACT.Startup
{
    [DependsOn(typeof(RMACTCoreModule))]
    public class RMACTGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RMACTGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}