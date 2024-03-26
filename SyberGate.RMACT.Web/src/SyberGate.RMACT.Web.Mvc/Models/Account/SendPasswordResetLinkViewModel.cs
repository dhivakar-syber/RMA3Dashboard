using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}