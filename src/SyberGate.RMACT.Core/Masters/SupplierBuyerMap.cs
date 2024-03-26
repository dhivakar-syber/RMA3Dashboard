using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
    [Table("SupplierBuyerMap")]
    [Audited]
    public class SupplierBuyerMap : Entity
    {
        [Required]
        public virtual int BuyerId { get; set; }

        [Required]
        public virtual int SupplierId { get; set; }

        [Required]
        [StringLength(BuyerConsts.MaxCodeLength, MinimumLength = BuyerConsts.MinCodeLength)]
        public virtual string BuyerCode { get; set; }

        [Required]
        [StringLength(BuyerConsts.MaxNameLength, MinimumLength = BuyerConsts.MinNameLength)]
        public virtual string BuyerName { get; set; }

        [Required]
        [StringLength(SupplierConsts.MaxCodeLength, MinimumLength = SupplierConsts.MinCodeLength)]
        public virtual string SupplierCode { get; set; }

        [Required]
        [StringLength(SupplierConsts.MaxNameLength, MinimumLength = SupplierConsts.MinNameLength)]
        public virtual string SupplierName { get; set; }
    }
}
