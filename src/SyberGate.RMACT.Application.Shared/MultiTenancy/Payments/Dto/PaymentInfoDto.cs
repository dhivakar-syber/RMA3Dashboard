using SyberGate.RMACT.Editions.Dto;

namespace SyberGate.RMACT.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }

        public bool IsLessThanMinimumUpgradePaymentAmount()
        {
            return AdditionalPrice < RMACTConsts.MinimumUpgradePaymentAmount;
        }
    }
}
