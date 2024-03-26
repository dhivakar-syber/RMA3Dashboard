
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditRMGradeDto : EntityDto<int?>
    {

		[Required]
		[StringLength(RMGradeConsts.MaxNameLength, MinimumLength = RMGradeConsts.MinNameLength)]
		public string Name { get; set; }
		
		

    }
}