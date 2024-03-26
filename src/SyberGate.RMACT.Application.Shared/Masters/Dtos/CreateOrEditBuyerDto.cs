
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditBuyerDto : EntityDto<int?>
    {

		[Required]
		[StringLength(BuyerConsts.MaxCodeLength, MinimumLength = BuyerConsts.MinCodeLength)]
		public string Code { get; set; }
		
		
		[Required]
		[StringLength(BuyerConsts.MaxNameLength, MinimumLength = BuyerConsts.MinNameLength)]
		public string Name { get; set; }
		
		
		 public long? UserId { get; set; }
		 public string Department {  get; set; }
		 
    }
}