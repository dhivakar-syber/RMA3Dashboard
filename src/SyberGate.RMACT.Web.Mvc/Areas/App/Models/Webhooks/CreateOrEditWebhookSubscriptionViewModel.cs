using Abp.Application.Services.Dto;
using Abp.Webhooks;
using SyberGate.RMACT.WebHooks.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
