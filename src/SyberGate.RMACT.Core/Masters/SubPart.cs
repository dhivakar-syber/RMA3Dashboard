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
	[Table("SubParts")]
	[Audited]
	public class SubPart : Entity
	{
		[Column(TypeName = "decimal(18,5)")]
		public virtual decimal? GrossInputWeight { get; set; }

		[Column(TypeName = "decimal(18,5)")]
		public virtual decimal? CastingForgingWeight { get; set; }

		[Column(TypeName = "decimal(18,5)")]
		public virtual decimal? FinishedWeight { get; set; }

		public virtual double? ScrapRecoveryPercent { get; set; }

		[Column(TypeName = "decimal(18,5)")]
		public virtual decimal? RMReferenceCost { get; set; }

		[Column(TypeName = "decimal(18,5)")]
		public virtual decimal? ConversionCost { get; set; }

		//[Column(TypeName = "decimal(18,5)")]
		//public virtual decimal? LogisticsCost { get; set; }

		[StringLength(PartConsts.MaxPartNoLength, MinimumLength = PartConsts.MinPartNoLength)]
		public virtual string PartNo { get; set; }

		public virtual string ParentPartNo { get; set; }

		[Required]
		[StringLength(PartConsts.MaxDescriptionLength, MinimumLength = PartConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }

		public virtual int? ParentPartId { get; set; }

		[ForeignKey("ParentPartId")]
		public Part PartFk { get; set; }

		public virtual int? SupplierId { get; set; }

		[ForeignKey("SupplierId")]
		public Supplier SupplierFk { get; set; }

		public virtual int BuyerId { get; set; }

		[ForeignKey("BuyerId")]
		public Buyer BuyerFk { get; set; }

		public virtual int? RMGroupId { get; set; }

		[ForeignKey("RMGroupId")]
		public RawMaterialGrade RMGroupFk { get; set; }

		public virtual int? RMGradeId { get; set; }

		[ForeignKey("RMGradeId")]
		public RawMaterialGrade RMGradeFk { get; set; }

		[StringLength(PartConsts.MaxDescriptionLength, MinimumLength = PartConsts.MinDescriptionLength)]
		public string RMReference { get; set; }
	}
}