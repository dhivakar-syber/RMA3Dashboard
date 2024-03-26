using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.IndexNames
{
    public class CreateOrEditIndexNameModalViewModel
    {
       public CreateOrEditIndexNameDto IndexName { get; set; }

	   
       
	   public bool IsEditMode => IndexName.Id.HasValue;
    }
}