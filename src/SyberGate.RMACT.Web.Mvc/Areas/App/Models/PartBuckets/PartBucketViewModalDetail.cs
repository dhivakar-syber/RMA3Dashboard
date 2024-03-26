using System.Collections.Generic;
using SyberGate.RMACT.Masters.Dtos;

namespace SyberGate.RMACT.Web.Areas.App.Models.PartBuckets
{
    public class PartBucketViewModalDetail
    {
        public string BuyerId { get; set; }
        public string SupplierId { get; set; }
        public string PartNo { get; set; }
        public string RawMaterial { get; set; }
        public decimal Price { get; set; }
        public bool IsCurrentPrice { get; set; }

        public List<PartBucketDto> RMBuckets { get; set; }
    }
}
