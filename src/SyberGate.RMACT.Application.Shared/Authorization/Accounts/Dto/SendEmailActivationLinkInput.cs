using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}