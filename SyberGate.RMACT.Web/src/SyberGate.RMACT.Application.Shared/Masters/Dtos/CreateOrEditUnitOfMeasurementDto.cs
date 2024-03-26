
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditUnitOfMeasurementDto : EntityDto<int?>
    {

		[Required]
		[StringLength(UnitOfMeasurementConsts.MaxCodeLength, MinimumLength = UnitOfMeasurementConsts.MinCodeLength)]
		public string Code { get; set; }
		
		
		[Required]
		[StringLength(UnitOfMeasurementConsts.MaxNameLength, MinimumLength = UnitOfMeasurementConsts.MinNameLength)]
		public string Name { get; set; }
		
		

    }
}