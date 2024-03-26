using SyberGate.RMACT.Editions;
using SyberGate.RMACT.Editions.Dto;
using SyberGate.RMACT.MultiTenancy.Payments;
using SyberGate.RMACT.Security;
using SyberGate.RMACT.MultiTenancy.Payments.Dto;

namespace SyberGate.RMACT.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
