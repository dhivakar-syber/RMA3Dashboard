using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("A3SubPartImpacts")]
	public class A3SubPartImpact : Entity
	{
        public virtual int DocId { get; set; }
        public virtual int? slno { get; set; }
        public virtual string ParentPartNo { get; set; }
        public virtual string PartNo { get; set; }
        public virtual string RawMaterialGrade { get; set; }
        public virtual string Description { get; set; }
        public virtual string ES1 { get; set; }
        public virtual string ES2 { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? ConversionCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? GrossInputWeight { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? CastingForgingWeight { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? FinishedWeight { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? ScrapRecovery { get; set; }
        public virtual double? ScrapRecoveryPercent { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? ScrapWeight { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? CurrentRMCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedRMCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? OtherCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? CurrentExwPrice { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedExwPrice { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? ExwPriceChangeInCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? ExwPriceChangeInPer { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? PackagingCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? LogisticsCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? CurrentFCAPrice { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedFCAPrice { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? CurrentAVOB { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedAVOB { get; set; }
        public virtual string PlantCode { get; set; }
        public virtual decimal? SOB { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? GlobusEPU { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RMImpact { get; set; }
        public virtual bool? IsParent { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? CurrentCostPer { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedCostPer { get; set; }

    }
}