using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dto
{
    public class GetTotalRate
    {
        public string BuyerId { get; set; }
        public string Department { get; set; }
        public string SupplierId { get; set; }
        public string RMGroupId { get; set; }
        public string RMGradeId { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
    
}
