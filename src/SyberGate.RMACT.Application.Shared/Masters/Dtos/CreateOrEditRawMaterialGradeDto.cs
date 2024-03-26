
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditRawMaterialGradeDto : EntityDto<int?>
    {

		[Required]
		[StringLength(RawMaterialGradeConsts.MaxNameLength, MinimumLength = RawMaterialGradeConsts.MinNameLength)]
		public string Name { get; set; }
		
		
		public bool IsGroup { get; set; }
		
		
		public bool HasMixture { get; set; }
		
		
		 public int? RawMaterialGradeId { get; set; }
		 
		 
    }
}