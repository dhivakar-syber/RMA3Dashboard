using SyberGate.RMACT.Models.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.MedicalSpecialties
{
    public class CreateOrEditMedicalSpecialtyModalViewModel
    {
        public CreateOrEditMedicalSpecialtyDto MedicalSpecialty { get; set; }

        public bool IsEditMode => MedicalSpecialty.Id.HasValue;
    }
}