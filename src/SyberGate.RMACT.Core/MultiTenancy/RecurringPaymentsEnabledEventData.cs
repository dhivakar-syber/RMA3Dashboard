using Abp.Events.Bus;

namespace SyberGate.RMACT.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}