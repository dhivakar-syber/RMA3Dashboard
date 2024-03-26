using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SyberGate.RMACT.Masters
{
    [Table("A3PartBuckets")]
    public class A3PartBucket : Entity
    {
        public virtual int DocId { get; set; }
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


    }
}
