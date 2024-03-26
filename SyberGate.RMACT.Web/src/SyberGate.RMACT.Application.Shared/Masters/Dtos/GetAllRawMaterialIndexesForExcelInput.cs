using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllRawMaterialIndexesForExcelInput
    {
		public string Filter { get; set; }

		public int? MonthFilter { get; set; }


		 public string IndexNameNameFilter { get; set; }

		 		 public string YearNameFilter { get; set; }

		 		 public string RawMaterialGradeNameFilter { get; set; }

		 
    }
}