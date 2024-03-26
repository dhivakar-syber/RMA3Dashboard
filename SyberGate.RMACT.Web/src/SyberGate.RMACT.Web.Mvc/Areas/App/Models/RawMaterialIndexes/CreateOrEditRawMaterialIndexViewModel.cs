using SyberGate.RMACT.Masters.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.RawMaterialIndexes
{
    public class CreateOrEditRawMaterialIndexModalViewModel
    {
       public CreateOrEditRawMaterialIndexDto RawMaterialIndex { get; set; }

	   		public string IndexNameName { get; set;}

		public string YearName { get; set;}

		public string RawMaterialGradeName { get; set;}


       public List<RawMaterialIndexIndexNameLookupTableDto> RawMaterialIndexIndexNameList { get; set;}

public List<RawMaterialIndexYearLookupTableDto> RawMaterialIndexYearList { get; set;}


	   public bool IsEditMode => RawMaterialIndex.Id.HasValue;
    }
}