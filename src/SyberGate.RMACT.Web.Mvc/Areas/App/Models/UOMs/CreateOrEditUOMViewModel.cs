using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.UOMs
{
    public class CreateOrEditUOMModalViewModel
    {
       public CreateOrEditUOMDto UOM { get; set; }

	   
       
	   public bool IsEditMode => UOM.Id.HasValue;
    }
}