using System.Collections.Generic;
using SyberGate.RMACT.Editions.Dto;
using SyberGate.RMACT.MultiTenancy.Payments;

namespace SyberGate.RMACT.Web.Models.Payment
{
    public class UpgradeEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public PaymentPeriodType PaymentPeriodType { get; set; }

        public SubscriptionPaymentType SubscriptionPaymentType { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}