using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.ApprovalUsers
{
    public class CreateOrEditApprovalUserModalViewModel
    {
       public CreateOrEditApprovalUserDto ApprovalUser { get; set; }

	   
       
       
	   public bool IsEditMode => ApprovalUser.Id.HasValue;
    }
}