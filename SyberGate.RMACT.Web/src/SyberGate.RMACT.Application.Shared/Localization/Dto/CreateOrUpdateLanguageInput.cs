using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}