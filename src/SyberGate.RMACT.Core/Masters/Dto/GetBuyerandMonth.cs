using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dto
{
    public class GetBuyerandMonth
    {
        public string Team {  get; set; }
        public string BuyerName { get; set; }
        public string Suppliername {  get; set; }
        public string Spec { get; set; }
        public decimal? WeightedAverageEPU { get; set; }
        public decimal? UnitRateAverage { get; set; }
        public decimal? January { get; set; }
        public decimal? February { get; set; }
        public decimal? March { get; set; }
        public decimal? April { get; set; }
        public decimal? May { get; set; }
        public decimal? June { get; set; }
        public decimal? July { get; set; }
        public decimal? August { get; set; }
        public decimal? September { get; set; }
        public decimal? October { get; set; }
        public decimal? November { get; set; }
        public decimal? December { get; set; }
    }
}
