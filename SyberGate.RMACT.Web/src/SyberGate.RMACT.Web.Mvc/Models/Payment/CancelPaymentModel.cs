using SyberGate.RMACT.MultiTenancy.Payments;

namespace SyberGate.RMACT.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}