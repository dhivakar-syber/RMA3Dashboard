
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditSupplierDto : EntityDto<int?>
    {

		[Required]
		[StringLength(SupplierConsts.MaxCodeLength, MinimumLength = SupplierConsts.MinCodeLength)]
		public string Code { get; set; }
		
		
		[Required]
		[StringLength(SupplierConsts.MaxNameLength, MinimumLength = SupplierConsts.MinNameLength)]
		public string Name { get; set; }
		
		

    }
}