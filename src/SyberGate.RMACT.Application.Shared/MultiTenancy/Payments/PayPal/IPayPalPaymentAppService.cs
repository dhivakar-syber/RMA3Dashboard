using System.Threading.Tasks;
using Abp.Application.Services;
using SyberGate.RMACT.MultiTenancy.Payments.PayPal.Dto;

namespace SyberGate.RMACT.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
