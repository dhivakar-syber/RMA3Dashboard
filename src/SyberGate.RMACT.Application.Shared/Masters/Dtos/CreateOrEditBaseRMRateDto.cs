using SyberGate.RMACT.Masters;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class CreateOrEditBaseRMRateDto : EntityDto<int?>
    {

		public decimal UnitRate { get; set; }

		[Range(RawMaterialMixtureConsts.MinWeightRatioValue, RawMaterialMixtureConsts.MaxWeightRatioValue)]
		public decimal WeightRatio { get; set; }


		[Range(RawMaterialMixtureConsts.MinLossRatioValue, RawMaterialMixtureConsts.MaxLossRatioValue)]
		public decimal LossRatio { get; set; }

		public double ScrapPercent { get; set; }

		public decimal ScrapAmount { get; set; }
		
		public Months Month { get; set; }
		
		
		 public int? RMGroupId { get; set; }
		 
		 		 public int? UnitOfMeasurementId { get; set; }
		 
		 		 public int? YearId { get; set; }
		 
		 		 public int? BuyerId { get; set; }
		 
		 		 public int? SupplierId { get; set; }

		public string IndexName { get; set; }

		public string FromPeriod { get; set; }

		public string ToPeriod { get; set; }
		
		public decimal IndexValue { get; set; }
		 
		public DateTime SettledDate { get; set; }
    }
}