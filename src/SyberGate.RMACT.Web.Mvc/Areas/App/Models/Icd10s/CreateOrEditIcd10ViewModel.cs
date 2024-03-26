using SyberGate.RMACT.Models.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Icd10s
{
    public class CreateOrEditIcd10ModalViewModel
    {
        public CreateOrEditIcd10Dto Icd10 { get; set; }

        public bool IsEditMode => Icd10.Id.HasValue;
    }
}