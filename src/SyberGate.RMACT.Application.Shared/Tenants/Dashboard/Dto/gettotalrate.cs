using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Tenants.Dashboard.Dto
{
    public class gettotalrate
    {
        public string Partno { get; set; }
        public string ParentGroup { get; set; }
        public string Spec { get; set; }
        public int GroupId {  get; set; }
        public int SpecId {  get; set; }
        public decimal? TotalAverage { get; set; }
        public decimal? TotalTon { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? TotalTonByGrade { get; set; }
        public decimal? TotalValueByGrade { get; set; }
        public decimal? GrossInputWeightAverage { get; set; }
        public decimal? EPUAverage { get; set; }
        public decimal? UnitRateAverage { get; set; }
        public decimal? SOBAverage { get; set; }

    }
}
