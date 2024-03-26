using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SyberGate.RMACT
{
    [DependsOn(typeof(RMACTCoreSharedModule))]
    public class RMACTApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RMACTApplicationSharedModule).GetAssembly());
        }
    }
}