using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace SyberGate.RMACT.Masters
{
    [Table("GlobusData")]
    [Audited]
    public class GlobusData: FullAuditedEntity<int> 
    {
        [StringLength(PartConsts.MaxPartNoLength, MinimumLength = PartConsts.MinPartNoLength)]
        public string PartNo { get; set; }

        [Required]
        [StringLength(PartConsts.MaxDescriptionLength, MinimumLength = PartConsts.MinDescriptionLength)]
        public string Description { get; set; }

        public virtual string ES1 { get; set; }
        public virtual string ES2 { get; set; }

        public string SupplierCode { get; set; }
        public string SuppliserName { get; set; }
        public decimal CurrentExwPrice { get; set; }
        public string PriceCurrency { get; set; }
        public string Uom { get; set; }
        public string Buyer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal PackagingCost { get; set; }
        public decimal LogisticsCost { get; set; }
        public string PlantCode { get; set; }
        public string PlantDescription { get; set; }
        public string ContractNo { get; set; }
        public decimal SOB { get; set; }
        public decimal EPU { get; set; }
        public string Status { get; set; }

        public virtual int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier SupplierFk { get; set; }

        public virtual int BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public Buyer BuyerFk { get; set; }
    }
}
