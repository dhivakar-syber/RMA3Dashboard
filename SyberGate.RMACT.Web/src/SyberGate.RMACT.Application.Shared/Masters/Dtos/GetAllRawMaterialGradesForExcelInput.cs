using Abp.Application.Services.Dto;
using System;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetAllRawMaterialGradesForExcelInput
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public int IsGroupFilter { get; set; }

		public int HasMixtureFilter { get; set; }


		 public string RawMaterialGradeNameFilter { get; set; }

		 
    }
}