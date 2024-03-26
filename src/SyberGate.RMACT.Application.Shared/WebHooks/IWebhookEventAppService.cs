using System.Threading.Tasks;
using Abp.Webhooks;

namespace SyberGate.RMACT.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
