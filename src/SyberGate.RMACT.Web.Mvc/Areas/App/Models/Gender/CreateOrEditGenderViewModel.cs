using SyberGate.RMACT.Models.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Gender
{
    public class CreateOrEditGenderModalViewModel
    {
        public CreateOrEditGenderDto Gender { get; set; }

        public bool IsEditMode => Gender.Id.HasValue;
    }
}