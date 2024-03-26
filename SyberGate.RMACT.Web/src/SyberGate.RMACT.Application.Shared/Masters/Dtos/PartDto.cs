
using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class PartDto : EntityDto
    {
		public decimal? GrossInputWeight { get; set; }

		public decimal? CastingForgingWeight { get; set; }

		public decimal? FinishedWeight { get; set; }

		public double? ScrapRecovery { get; set; }

		public double? ScrapRecoveryPercent { get; set; }

		public decimal? RMReferenceCost { get; set; }

		public decimal? ConversionCost { get; set; }

		public string PartNo { get; set; }

		public string SubPartNo { get; set; }

		public string SubPartDescription { get; set; }

		public string Description { get; set; }

		public string RMReference { get; set; }

		public bool? IsParent { get; set; }

		 public int? SupplierId { get; set; }

		 		 public int BuyerId { get; set; }

		 		 public int? RMGroupId { get; set; }

		 
    }
}