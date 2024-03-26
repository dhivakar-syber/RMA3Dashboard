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
	[Table("RawMaterialMixtures")]
    [Audited]
    public class RawMaterialMixture : Entity 
    {

		[Range(RawMaterialMixtureConsts.MinWeightRatioValue, RawMaterialMixtureConsts.MaxWeightRatioValue)]
		public virtual decimal? WeightRatio { get; set; }
		
		[Range(RawMaterialMixtureConsts.MinLossRatioValue, RawMaterialMixtureConsts.MaxLossRatioValue)]
		public virtual decimal? LossRatio { get; set; }
		

		public virtual int RMGroupId { get; set; }
		
        [ForeignKey("RMGroupId")]
		public RMGroup RMGroupFk { get; set; }
		
		public virtual int RawMaterialGradeId { get; set; }
		
        [ForeignKey("RawMaterialGradeId")]
		public RawMaterialGrade RawMaterialGradeFk { get; set; }
		
		public virtual int BuyerId { get; set; }
		
        [ForeignKey("BuyerId")]
		public Buyer BuyerFk { get; set; }
		
		public virtual int SupplierId { get; set; }
		
        [ForeignKey("SupplierId")]
		public Supplier SupplierFk { get; set; }
		
    }
}