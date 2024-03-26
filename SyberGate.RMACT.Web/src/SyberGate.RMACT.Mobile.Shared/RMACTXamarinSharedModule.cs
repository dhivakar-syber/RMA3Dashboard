using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SyberGate.RMACT
{
    [DependsOn(typeof(RMACTClientModule), typeof(AbpAutoMapperModule))]
    public class RMACTXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RMACTXamarinSharedModule).GetAssembly());
        }
    }
}