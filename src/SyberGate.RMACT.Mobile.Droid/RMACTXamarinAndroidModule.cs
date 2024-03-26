using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SyberGate.RMACT
{
    [DependsOn(typeof(RMACTXamarinSharedModule))]
    public class RMACTXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RMACTXamarinAndroidModule).GetAssembly());
        }
    }
}