using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("ProcureDatas")]
    public class ProcureData : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[StringLength(ProcureDataConsts.MaxPartNoLength, MinimumLength = ProcureDataConsts.MinPartNoLength)]
		public virtual string PartNo { get; set; }
		
		[Required]
		[StringLength(ProcureDataConsts.MaxDescriptionLength, MinimumLength = ProcureDataConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		
		public virtual string SupplierCode { get; set; }
		
		public virtual string SuppliserName { get; set; }
		
		[Required]
		public virtual decimal CurrentExwPrice { get; set; }
		
		public virtual string PriceCurrency { get; set; }
		
		public virtual string Uom { get; set; }
		
		public virtual string Buyer { get; set; }
		
		[Required]
		public virtual DateTime FromDate { get; set; }
		
		[Required]
		public virtual DateTime ToDate { get; set; }
		
		[Required]
		public virtual decimal PackagingCost { get; set; }
		
		[Required]
		public virtual decimal LogisticsCost { get; set; }
		
		public virtual string PlantCode { get; set; }
		
		public virtual string PlantDescription { get; set; }
		
		public virtual string ContractNo { get; set; }
		
		[Required]
		public virtual decimal SOB { get; set; }
		
		[Required]
		public virtual decimal EPU { get; set; }
		
		public virtual string Status { get; set; }
		
		[Required]
		public virtual DateTime CreationTime { get; set; }
		
		public virtual DateTime? DeletionTime { get; set; }
		
		[Required]
		public virtual bool IsDeleted { get; set; }
		
		public virtual DateTime? LastModificationTime { get; set; }
		

    }
}