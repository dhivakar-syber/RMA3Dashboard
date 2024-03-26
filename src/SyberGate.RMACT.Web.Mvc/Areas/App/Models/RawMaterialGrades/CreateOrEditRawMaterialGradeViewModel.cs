using SyberGate.RMACT.Masters.Dtos;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.RawMaterialGrades
{
    public class CreateOrEditRawMaterialGradeModalViewModel
    {
       public CreateOrEditRawMaterialGradeDto RawMaterialGrade { get; set; }

	   		public string RawMaterialGradeName { get; set;}


       
	   public bool IsEditMode => RawMaterialGrade.Id.HasValue;
    }
}