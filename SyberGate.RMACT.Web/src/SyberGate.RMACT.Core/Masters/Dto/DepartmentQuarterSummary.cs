using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dto
{
    public class DepartmentQuarterSummary
    {
        public string Department { get; set; }
        public int Q1_Total { get; set; }
        public int Q1_Approved { get; set; }
        public int Q1_Pending { get; set; }
        public int Q2_Total { get; set; }
        public int Q2_Approved { get; set; }
        public int Q2_Pending { get; set; }
        public int Q3_Total { get; set; }
        public int Q3_Approved { get; set; }
        public int Q3_Pending { get; set; }
        public int Q4_Total { get; set; }
        public int Q4_Approved { get; set; }
        public int Q4_Pending { get; set; }
    }
}
