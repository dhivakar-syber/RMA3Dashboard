using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.RMGroups
{
    public class CreateOrEditRMGroupModalViewModel
    {
       public CreateOrEditRMGroupDto RMGroup { get; set; }

	   
       
	   public bool IsEditMode => RMGroup.Id.HasValue;
    }
}