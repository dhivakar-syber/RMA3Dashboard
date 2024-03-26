using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace SyberGate.RMACT.Masters.Dto
{
    public class GetA3DashboardDataInput
    {
        public string Supplier { get; set; }
        public string Buyer { get; set; }
        public string Department { get; set; }
        public string Period { get; set; }
        public string Partno { get; set; }
        public bool IsGenerateA3 { get; set; }
        public int A3Id { get; set; }
        public string SupplierName { get; set; }
        public string BuyerName { get; set; }
        public string Plant { get; set; }
        public string TemplatePath { get; set; }
        public string Group { get; set;  }

        public string Grade { get; set; }

        public string Spec { get; set;}

        public string Year { get; set; }

        public string Month { get; set; }

        public int? GradeId { get; set; }
    }
}
        