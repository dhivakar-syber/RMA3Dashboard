using SyberGate.RMACT.Models.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Constants
{
    public class CreateOrEditConstantModalViewModel
    {
        public CreateOrEditConstantDto Constant { get; set; }

        public bool IsEditMode => Constant.Id.HasValue;
    }
}