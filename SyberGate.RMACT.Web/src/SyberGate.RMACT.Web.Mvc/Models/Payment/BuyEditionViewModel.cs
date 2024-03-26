using System.Collections.Generic;
using SyberGate.RMACT.Editions;
using SyberGate.RMACT.Editions.Dto;
using SyberGate.RMACT.MultiTenancy.Payments;
using SyberGate.RMACT.MultiTenancy.Payments.Dto;

namespace SyberGate.RMACT.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
