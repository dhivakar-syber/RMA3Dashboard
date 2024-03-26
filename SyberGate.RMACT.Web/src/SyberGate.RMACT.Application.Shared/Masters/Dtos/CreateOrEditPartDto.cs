
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditPartDto : EntityDto<int?>
    {

		public decimal? GrossInputWeight { get; set; }
		
		
		public decimal? CastingForgingWeight { get; set; }
		
		
		public decimal? FinishedWeight { get; set; }
		
		
		public double? ScrapRecoveryPercent { get; set; }
		
		
		[StringLength(PartConsts.MaxPartNoLength, MinimumLength = PartConsts.MinPartNoLength)]
		public string PartNo { get; set; }
		
		
		[Required]
		[StringLength(PartConsts.MaxDescriptionLength, MinimumLength = PartConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		
		 public int? SupplierId { get; set; }
		 
		 		 public int BuyerId { get; set; }
		 
		 		 public int? RMGroupId { get; set; }

		public decimal? RMReferenceCost { get; set; }

		public decimal? ConversionCost { get; set; }


	}
}