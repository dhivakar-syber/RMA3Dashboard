using SyberGate.RMACT.Masters;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditRawMaterialIndexDto : EntityDto<int?>
    {

		public Months Month { get; set; }
		
		
		public decimal? Value { get; set; }
		
		
		 public int IndexNameId { get; set; }
		 
		 		 public int YearId { get; set; }
		 
		 		 public int RawMaterialGradeId { get; set; }
		 
		 
    }
}