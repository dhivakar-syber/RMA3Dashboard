using System;
using Abp.AutoMapper;

namespace SyberGate.RMACT.Masters.Dto
{
    public class GetPriceTrendsp
    {
        public int id { get; set; }
        public int RMGroupId { get; set; }
        public string RMGrade { get; set; }
        public string Uom { get; set; }
        public string SetteledMY { get; set; }
        public decimal SetteledUR { get; set; }
        public string RevisedMY { get; set; }
        public decimal RevisedUR { get; set; }
        public string BaseRMPOC { get; set; }
        public decimal ScrapSetteled { get; set; }
        public decimal ScrapRevised { get; set; }
        public string ScrapPOC { get; set; }
        public bool RevApproved { get; set; }
        public bool SetApproved { get; set; }
        public int SetId { get; set; }
        public int UOMId { get; set; }
        public int RMGradeId { get; set; }
        public string RevIndexName { get; set; }
        public string RevFromPeriod { get; set; }
        public string RevToPeriod { get; set; }
        public decimal RevIndexValue { get; set; }
        public string SetIndexName { get; set; }
        public string SetFromPeriod { get; set; }
        public string SetToPeriod { get; set; }
        public decimal SetIndexValue { get; set; }
        public string MixtureGrade { get; set; }

        public string ParentGrp { get; set; }
        public decimal SetWRatio { get; set; }
        public decimal SetLRatio { get; set; }
        public decimal RevWRatio { get; set; }
        public decimal RevLRatio { get; set; }

        public string SettledDate { get; set; }
        
        public string RevisedDate { get; set; }
        
    }   
}
