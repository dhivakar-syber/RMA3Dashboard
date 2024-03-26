using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Parts
{
    public class CreateOrEditPartModalViewModel
    {
       public CreateOrEditPartDto Part { get; set; }

	   		public string SupplierName { get; set;}

		public string BuyerName { get; set;}

		public string RMGroupName { get; set;}


       
	   public bool IsEditMode => Part.Id.HasValue;
    }
}