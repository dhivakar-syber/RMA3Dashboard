using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace SyberGate.RMACT.Masters.Importing.Dto
{
    [AutoMap(typeof(GlobusData))]
    public class ImportGlobusDataDto
    {
        public string PartNo { get; set; }
        public string ES1 { get; set;  }
        public string ES2 { get; set; }
        public string Description { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public decimal CurrentExwPrice { get; set; }
        public string PriceCurrency { get; set; }
        public string Uom { get; set; }
        public string Buyer { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal? PackagingCost { get; set; }
        public decimal? LogisticsCost { get; set; }
        public string PlantCode { get; set; }
        public string PlantDescription { get; set; }
        public string ContractNo { get; set; }
        public string SOB { get; set; }
        public string EPU { get; set; }
        public string Status { get; set; }

        public string Exception { get; set; }

        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
