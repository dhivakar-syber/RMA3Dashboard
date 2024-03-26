using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("A3PriceImpacts")]
	public class A3PriceImpact : Entity
	{
        public virtual int DocId { get; set; }
        public virtual int slno { get; set; }
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

        public string RMReference { get; set; }

        public bool SubMixture { get; set; }

        public bool SubPart { get; set; }

        public bool IsParentSubMixture { get; set; }

        public bool IsParentMixture { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal BaseRMRate { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RMSurchargeGradeDiff { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal SecondaryProcessing { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal SurfaceProtection { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Thickness { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal CuttingCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal MOQVolume { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Transport { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal Others { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevBaseRMRate { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevRMSurchargeGradeDiff { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevSecondaryProcessing { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevSurfaceProtection { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevThickness { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevCuttingCost { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevMOQVolume { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevTransport { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RevOthers { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal RMImpactt { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal ProcessImpact { get; set; }

        public string RawMaterialGroup { get; set; }


    }
}