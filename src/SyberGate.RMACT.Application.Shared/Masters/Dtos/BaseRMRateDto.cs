using SyberGate.RMACT.Masters;

using System;
using Abp.Application.Services.Dto;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class BaseRMRateDto : EntityDto
    {
		public decimal UnitRate { get; set; }

		public double ScrapPercent { get; set; }

		public decimal ScrapAmount { get; set; }

		public decimal? WeightRatio { get; set; }

		public decimal? LossRatio { get; set; }

		public Months Month { get; set; }

		public bool IsApproved { get; set; }

		 public int? RMGroupId { get; set; }

		 		 public int? UnitOfMeasurementId { get; set; }

		 		 public int? YearId { get; set; }

		 		 public int? BuyerId { get; set; }

		 		 public int? SupplierId { get; set; }

		public string IndexName { get; set; }

		public string FromPeriod { get; set; }

		public string ToPeriod { get; set; }

		public decimal IndexValue { get; set; }

		public string SettledDate { get; set; }

	}
}