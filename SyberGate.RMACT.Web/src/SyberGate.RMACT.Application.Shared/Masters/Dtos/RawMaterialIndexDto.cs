using SyberGate.RMACT.Masters;

using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class RawMaterialIndexDto : EntityDto
    {
		public Months Month { get; set; }

		public decimal? Value { get; set; }


		 public int IndexNameId { get; set; }

		 		 public int YearId { get; set; }

		 		 public int RawMaterialGradeId { get; set; }

		 
    }
}