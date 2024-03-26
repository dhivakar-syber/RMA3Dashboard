using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Plants
{
    public class CreateOrEditPlantModalViewModel
    {
       public CreateOrEditPlantDto Plant { get; set; }

	   
       
	   public bool IsEditMode => Plant.Id.HasValue;
    }
}