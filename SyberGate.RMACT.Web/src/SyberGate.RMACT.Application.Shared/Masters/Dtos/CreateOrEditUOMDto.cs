
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditUOMDto : EntityDto<int?>
    {

		[Required]
		[StringLength(UOMConsts.MaxCodeLength, MinimumLength = UOMConsts.MinCodeLength)]
		public string Code { get; set; }
		
		
		[Required]
		[StringLength(UOMConsts.MaxDescriptionLength, MinimumLength = UOMConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		

    }
}