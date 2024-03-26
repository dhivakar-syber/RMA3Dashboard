using SyberGate.RMACT.Models.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Ethnicities
{
    public class CreateOrEditEthnicityModalViewModel
    {
       public CreateOrEditEthnicityDto Ethnicity { get; set; }

	   
       
	   public bool IsEditMode => Ethnicity.Id.HasValue;
    }
}