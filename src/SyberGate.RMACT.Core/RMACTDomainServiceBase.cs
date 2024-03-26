using Abp.Domain.Services;

namespace SyberGate.RMACT
{
    public abstract class RMACTDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected RMACTDomainServiceBase()
        {
            LocalizationSourceName = RMACTConsts.LocalizationSourceName;
        }
    }
}
