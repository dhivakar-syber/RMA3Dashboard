using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace SyberGate.RMACT.Masters
{
	[Table("PartBuckets")]
    public class PartBucket : Entity 
    {

        //[Required]
        //[StringLength(PartBucketConsts.MaxPartNumberLength, MinimumLength = PartBucketConsts.MinPartNumberLength)]
        //public virtual string PartNumber { get; set; }

        [Required]
        [StringLength(PartBucketConsts.MaxRMSpecLength, MinimumLength = PartBucketConsts.MinRMSpecLength)]
        public virtual string RMSpec { get; set; }


        [Required]
		[StringLength(PartBucketConsts.MaxBucketsLength, MinimumLength = PartBucketConsts.MinBucketsLength)]
		public virtual string Buckets { get; set; }
		
		public virtual decimal Value { get; set; }
		
		[StringLength(PartBucketConsts.MaxBuyerLength, MinimumLength = PartBucketConsts.MinBuyerLength)]
		public virtual string Buyer { get; set; }
		
		[StringLength(PartBucketConsts.MaxSupplierLength, MinimumLength = PartBucketConsts.MinSupplierLength)]
		public virtual string Supplier { get; set; }

		[Required]
		public virtual DateTime CreatedOn { get; set; }

        [StringLength(PartBucketConsts.MaxSupplierLength, MinimumLength = PartBucketConsts.MinSupplierLength)]
        public virtual string Month { get; set; }

        [StringLength(PartBucketConsts.MaxSupplierLength, MinimumLength = PartBucketConsts.MinSupplierLength)]
        public virtual string Year { get; set; }

        public virtual int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier SupplierFk { get; set; }

        public virtual int BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public Buyer BuyerFk { get; set; }


    }
}