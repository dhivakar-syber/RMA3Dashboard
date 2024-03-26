using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.LeadModels
{
    public class CreateOrEditLeadModelModalViewModel
    {
       public CreateOrEditLeadModelDto LeadModel { get; set; }

	   
       
	   public bool IsEditMode => LeadModel.Id.HasValue;
    }
}