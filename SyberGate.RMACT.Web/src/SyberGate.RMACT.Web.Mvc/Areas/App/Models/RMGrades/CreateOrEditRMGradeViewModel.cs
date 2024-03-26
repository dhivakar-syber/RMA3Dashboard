using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.RMGrades
{
    public class CreateOrEditRMGradeModalViewModel
    {
       public CreateOrEditRMGradeDto RMGrade { get; set; }

	   
       
	   public bool IsEditMode => RMGrade.Id.HasValue;
    }
}