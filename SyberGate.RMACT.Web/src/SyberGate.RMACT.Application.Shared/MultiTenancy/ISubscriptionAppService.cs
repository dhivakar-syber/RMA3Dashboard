using System.Threading.Tasks;
using Abp.Application.Services;

namespace SyberGate.RMACT.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
