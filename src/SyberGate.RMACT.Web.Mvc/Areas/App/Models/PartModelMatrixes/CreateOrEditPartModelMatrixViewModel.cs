using SyberGate.RMACT.Masters.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.PartModelMatrixes
{
    public class CreateOrEditPartModelMatrixModalViewModel
    {
       public CreateOrEditPartModelMatrixDto PartModelMatrix { get; set; }

	   		public string LeadModelName { get; set;}


       public List<PartModelMatrixLeadModelLookupTableDto> PartModelMatrixLeadModelList { get; set;}


	   public bool IsEditMode => PartModelMatrix.Id.HasValue;
    }
}