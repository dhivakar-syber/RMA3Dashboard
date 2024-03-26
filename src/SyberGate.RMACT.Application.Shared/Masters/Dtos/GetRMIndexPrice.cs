using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetRMIndexPrice
    {
        public string RMGrade { get; set; }
        public int RMGradeId { get; set; }
        public string IndexName { get; set; }
        public int IndexNameId { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public decimal Value { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
    }
}
