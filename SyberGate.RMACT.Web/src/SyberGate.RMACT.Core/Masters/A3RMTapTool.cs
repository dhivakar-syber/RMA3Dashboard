﻿using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SyberGate.RMACT.Masters
{
    [Table("A3RMTapTool")]
    public class A3RMTapTool : Entity
    {

        public virtual int DocId { get; set; }

        [StringLength(PartBucketConsts.MaxRMSpecLength, MinimumLength = PartBucketConsts.MinRMSpecLength)]
        public virtual string RMGrade { get; set; }

        [Required]
        [StringLength(PartBucketConsts.MaxRMSpecLength, MinimumLength = PartBucketConsts.MinRMSpecLength)]
        public virtual string RMSpec { get; set; }

        [StringLength(PartBucketConsts.MaxBuyerLength, MinimumLength = PartBucketConsts.MinBuyerLength)]
        public virtual string Buyer { get; set; }

        [StringLength(PartBucketConsts.MaxSupplierLength, MinimumLength = PartBucketConsts.MinSupplierLength)]
        public virtual string Supplier { get; set; }

        public virtual decimal BaseRMRate { get; set; }

        public virtual decimal RMSurchargeGradeDiff { get; set; }

        public virtual decimal SecondaryProcessing { get; set; }

        public virtual decimal SurfaceProtection { get; set; }

        public virtual decimal Thickness { get; set; }

        public virtual decimal CuttingCost { get; set; }

        public virtual decimal MOQVolume { get; set; }

        public virtual decimal Transport { get; set; }

        public virtual decimal Others { get; set; }



        [Required]
        public virtual DateTime CreatedOn { get; set; }

        [Required]
        public virtual DateTime Date { get; set; }

        public virtual int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier SupplierFk { get; set; }

        public virtual int BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public Buyer BuyerFk { get; set; }


    }
}