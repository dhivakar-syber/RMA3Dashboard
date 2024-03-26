using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SyberGate.RMACT
{
    public class RMACTCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RMACTCoreSharedModule).GetAssembly());
        }
    }
}