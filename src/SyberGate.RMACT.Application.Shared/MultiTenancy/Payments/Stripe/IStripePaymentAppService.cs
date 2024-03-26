using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.MultiTenancy.Payments.Dto;
using SyberGate.RMACT.MultiTenancy.Payments.Stripe.Dto;

namespace SyberGate.RMACT.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}