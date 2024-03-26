using SyberGate.RMACT.Models.Dtos;

namespace SyberGate.RMACT.Web.Areas.App.Models.Gender
{
    public class GenderViewModel : GetGenderForViewDto
    {
        public string FilterText { get; set; }
    }
}