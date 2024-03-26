
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditRMGroupDto : EntityDto<int?>
    {

		[Required]
		[StringLength(RMGroupConsts.MaxNameLength, MinimumLength = RMGroupConsts.MinNameLength)]
		public string Name { get; set; }
		
		
		public bool HasMixture { get; set; }
		
		

    }
}