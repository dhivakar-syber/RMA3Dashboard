using SyberGate.RMACT.Masters.Dtos;
using System.Collections.Generic;
using Abp.Extensions;

namespace SyberGate.RMACT.Web.Areas.App.Models.BaseRMRates
{
    public class BaseRMHistoryViewModel
    {
        public List<CreateOrEditBaseRMRateModalViewModel> RMHistory { get; set; }

        public int RMGroupId { get; set; }
        public int BuyerId { get; set; }
        public int SupplierId { get; set; } 
        public string RMGroup { get; set; }
        public string Buyer { get; set; }
        public string Supplier { get; set; }
        public string Period { get; set; }
    }
}
