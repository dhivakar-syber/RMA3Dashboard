using SyberGate.RMACT.Dto;

namespace SyberGate.RMACT.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
