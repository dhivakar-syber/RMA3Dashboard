using Abp;
using Microsoft.AspNetCore.Hosting;

namespace SyberGate.RMACT
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="RMACTDomainServiceBase"/>.
    /// For application services inherit RMACTAppServiceBase.
    /// </summary>
    public abstract class RMACTServiceBase : AbpServiceBase
    {
        protected RMACTServiceBase()
        {
            LocalizationSourceName = RMACTConsts.LocalizationSourceName;
        }
    }
}