
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditPlantDto : EntityDto<int?>
    {

		[Required]
		[StringLength(PlantConsts.MaxCodeLength, MinimumLength = PlantConsts.MinCodeLength)]
		public string Code { get; set; }
		
		
		[Required]
		[StringLength(PlantConsts.MaxDescriptionLength, MinimumLength = PlantConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		

    }
}