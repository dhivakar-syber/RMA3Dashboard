using SyberGate.RMACT.Models.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.MedicinalTypes
{
    public class CreateOrEditMedicinalTypeModalViewModel
    {
        public CreateOrEditMedicinalTypeDto MedicinalType { get; set; }

        public bool IsEditMode => MedicinalType.Id.HasValue;
    }
}