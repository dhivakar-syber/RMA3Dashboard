using System.Collections.Generic;
using SyberGate.RMACT.Editions.Dto;
using SyberGate.RMACT.MultiTenancy.Payments;

namespace SyberGate.RMACT.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}