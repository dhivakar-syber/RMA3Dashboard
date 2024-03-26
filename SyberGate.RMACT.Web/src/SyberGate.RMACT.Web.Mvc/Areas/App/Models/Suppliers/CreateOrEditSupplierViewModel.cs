using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Suppliers
{
    public class CreateOrEditSupplierModalViewModel
    {
       public CreateOrEditSupplierDto Supplier { get; set; }

	   
       
	   public bool IsEditMode => Supplier.Id.HasValue;
    }
}