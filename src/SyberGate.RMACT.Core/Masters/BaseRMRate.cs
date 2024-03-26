using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
	[Table("BaseRMRates")]
    [Audited]
    public class BaseRMRate : Entity 
    {
		[Column(TypeName = "decimal(18,5)")]
		public virtual decimal UnitRate { get; set; }

		[Column(TypeName = "decimal(18,5)")]
		public virtual decimal ScrapAmount { get; set; }

		[Range(RawMaterialMixtureConsts.MinWeightRatioValue, RawMaterialMixtureConsts.MaxWeightRatioValue)]
		public virtual decimal? WeightRatio { get; set; }

		[Range(RawMaterialMixtureConsts.MinLossRatioValue, RawMaterialMixtureConsts.MaxLossRatioValue)]
		public virtual decimal? LossRatio { get; set; }

		public virtual double ScrapPercent { get; set; }
		
		public virtual Months Month { get; set; }
		

		public virtual int? RMGroupId { get; set; }

		public virtual bool? IsApproved { get; set; }

		public string IndexName { get; set; }

		public string FromPeriod { get; set; }

		public string ToPeriod { get; set; }

		public decimal IndexValue { get; set; }

		[ForeignKey("RMGroupId")]
		public RawMaterialGrade RMGroupFk { get; set; }
		
		public virtual int? UnitOfMeasurementId { get; set; }
		
        [ForeignKey("UnitOfMeasurementId")]
		public UnitOfMeasurement UnitOfMeasurementFk { get; set; }
		
		public virtual int? YearId { get; set; }
		
        [ForeignKey("YearId")]
		public Year YearFk { get; set; }
		
		public virtual int? BuyerId { get; set; }
		
        [ForeignKey("BuyerId")]
		public Buyer BuyerFk { get; set; }
		
		public virtual int? SupplierId { get; set; }
		
        [ForeignKey("SupplierId")]
		public Supplier SupplierFk { get; set; }

		public DateTime SettledDate { get; set; }
		
    }
}