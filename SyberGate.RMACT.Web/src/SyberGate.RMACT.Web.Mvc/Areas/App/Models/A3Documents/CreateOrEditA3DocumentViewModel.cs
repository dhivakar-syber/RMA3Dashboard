using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.A3Documents
{
    public class CreateOrEditA3DocumentViewModel
    {
       public CreateOrEditA3DocumentDto A3Document { get; set; }

	   
       
	   public bool IsEditMode => A3Document.Id.HasValue;
    }
}