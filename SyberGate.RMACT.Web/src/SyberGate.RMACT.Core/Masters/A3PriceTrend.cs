using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("A3PriceTrends")]
    public class A3PriceTrend : Entity 
    {
        public virtual int DocId { get; set; }
        public virtual int? RevId { get; set; }
        public virtual int? RMGroupId { get; set; }
        public virtual string RMGrade { get; set; }
        public virtual string Uom { get; set; }
        public virtual string SetteledMY { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? SetteledUR { get; set; }
        public virtual string RevisedMY { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedUR { get; set; }
        public virtual string BaseRMPOC { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? ScrapSetteled { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? ScrapRevised { get; set; }
        public virtual string ScrapPOC { get; set; }
        public virtual bool? RevApproved { get; set; }
        public virtual bool? SetApproved { get; set; }
        public virtual int? SetId { get; set; }
        public string RevIndexName { get; set; }
        public string RevFromPeriod { get; set; }
        public string RevToPeriod { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal? RevIndexValue { get; set; }

        public virtual string MixtureGrade { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? SetteledWRatio { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? SetteledLRatio { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedWRatio { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public virtual decimal? RevisedLRatio { get; set; }

        public virtual bool? HasMixture { get; set; }
    }
}