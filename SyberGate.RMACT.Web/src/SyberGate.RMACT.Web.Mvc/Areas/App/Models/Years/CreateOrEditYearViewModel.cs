using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.Years
{
    public class CreateOrEditYearModalViewModel
    {
       public CreateOrEditYearDto Year { get; set; }

	   
       
	   public bool IsEditMode => Year.Id.HasValue;
    }
}