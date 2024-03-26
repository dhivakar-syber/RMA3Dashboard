using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SyberGate.RMACT
{
    public class RMACTClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RMACTClientModule).GetAssembly());
        }
    }
}
