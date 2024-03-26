using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
