using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.RawMaterialMixtures
{
    public class CreateOrEditRawMaterialMixtureModalViewModel
    {
       public CreateOrEditRawMaterialMixtureDto RawMaterialMixture { get; set; }

	   		public string RMGroupName { get; set;}

		public string RawMaterialGradeName { get; set;}

		public string BuyerName { get; set;}

		public string SupplierName { get; set;}


       
	   public bool IsEditMode => RawMaterialMixture.Id.HasValue;
    }
}