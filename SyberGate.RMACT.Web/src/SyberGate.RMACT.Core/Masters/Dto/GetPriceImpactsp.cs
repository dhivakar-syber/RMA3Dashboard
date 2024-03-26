using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dto
{
    public class GetPriceImpactsp
    {
        public string slno { get; set; }
        public string ParentPartNo { get; set; }
        public string PartNo { get; set; }
        public string ES1 { get; set; }
        public string ES2 { get; set; }

        public string RawMaterialGroup { get; set; }
        public string RawMaterialGrade { get; set; }
        public string Description { get; set; }
        public decimal GrossInputWeight { get; set; }
        public decimal CastingForgingWeight { get; set; }
        public decimal FinishedWeight { get; set; }
        public decimal ScrapRecovery { get; set; }
        public double ScrapRecoveryPercent { get; set; }
        public decimal ScrapWeight { get; set; }
        public decimal CurrentRMCost { get; set; }
        public decimal RevisedRMCost { get; set; }

        public decimal BaseRMRate { get; set; }
        public decimal RMSurchargeGradeDiff { get; set; }
        public decimal SecondaryProcessing { get; set; }
        public decimal SurfaceProtection { get; set; }
        public decimal Thickness { get; set; }
        public decimal CuttingCost { get; set; }
        public decimal MOQVolume { get; set; }
        public decimal Transport { get; set; }
        public decimal Others { get; set; }

        public decimal RevBaseRMRate { get; set; }
        public decimal RevRMSurchargeGradeDiff { get; set; }
        public decimal RevSecondaryProcessing { get; set; }
        public decimal RevSurfaceProtection { get; set; }
        public decimal RevThickness { get; set; }
        public decimal RevCuttingCost { get; set; }
        public decimal RevMOQVolume { get; set; }
        public decimal RevTransport { get; set; }
        public decimal RevOthers { get; set; }


        public decimal OtherCost { get; set; }
        public decimal CurrentExwPrice { get; set; }
        public decimal RevisedExwPrice { get; set; }
        public decimal ExwPriceChangeInCost { get; set; }
        public decimal ExwPriceChangeInPer { get; set; }
        public decimal PackagingCost { get; set; }
        public decimal LogisticsCost { get; set; }
        public decimal CurrentFCAPrice { get; set; }
        public decimal RevisedFCAPrice { get; set; }
        public decimal CurrentAVOB { get; set; }
        public decimal RevisedAVOB { get; set; }
        public string PlantCode { get; set; }
        public decimal SOB { get; set; }
        public decimal GlobusEPU { get; set; }
        public decimal RMImpact { get; set; }
        public bool IsParent { get; set; }
        public decimal ConversionCost { get; set; }
        public decimal CurrentCostPer { get; set; }
        public decimal RevisedCostPer { get; set; }
        public string RMReference { get; set; }

        public string Settleddate { get; set; }

        public string Reviseddate { get; set; }

        public bool Submixture { get; set; }

        public decimal RMImpactt { get; set; }

        public decimal ProcessImpact { get; set;}

        public bool SubPart { get; set; }

        public bool IsParentSubMixture { get; set; }

        public bool IsParentMixture { get; set; }

    }
}
