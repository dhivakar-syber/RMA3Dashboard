using System.Threading.Tasks;
using SyberGate.RMACT.Authorization.Users;

namespace SyberGate.RMACT.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
