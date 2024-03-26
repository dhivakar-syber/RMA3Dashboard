using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Tenants.Dashboard.Dto
{
    public class GetRMPriceTrend
    {
        public string SlNo { get; set; }
        public int Id { get; set; }
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
        public int UomId { get; set; }
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
        public decimal SetteledWRatio { get; set; }
        public decimal SetteledLRatio { get; set; }
        public decimal RevisedWRatio { get; set; }
        public decimal RevisedLRatio { get; set; }

        public string SettledDate { get; set; }

        public string RevisedDate { get; set; }

        public string ParentGrp { get; set; }

        public decimal BaseRMRate { get; set; }
        public decimal RMSurchargeGradeDiff { get; set; }
        public decimal SecondaryProcessing { get; set; }
        public decimal SurfaceProtection { get; set; }
        public decimal Thickness { get; set; }
        public decimal CuttingCost { get; set; }
        public decimal MOQVolume { get; set; }
        public decimal Transport { get; set; }
        public decimal Others { get; set; }
        public string Supplier { get; set; }
        public string SupplierId { get; set; }
        public int BuyerId { get; set; }
        public string Department { get; set; }
        public string Buyer { get; set; }
        public string Partno { get; set; }
        public decimal? TotalAverage { get; set; }
        public decimal? GrossInputWeightAverage { get; set; }
        public decimal? EPUAverage { get; set; }
        public decimal? UnitRateAverage { get; set; }
        public decimal? SOBAverage { get; set; }
        //public string Supplier { get; set; }
        public string SupplierCode { get; set; }
        public decimal TotalAVOB { get; set; }
        public decimal TotalAVOBBySupplier { get; set; }
        public int TotalBuyers { get; set; }
        public int TotalParts { get; set; }

    }
}
