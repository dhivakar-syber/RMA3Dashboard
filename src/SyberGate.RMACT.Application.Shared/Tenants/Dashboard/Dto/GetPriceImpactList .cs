using System.Collections.Generic;

namespace SyberGate.RMACT.Tenants.Dashboard.Dto
{
    public class GetPriceImpactList
    {
        public GetPriceImpactList(List<GetRMPriceImpact> priceImpacts)
        {
            PriceImpacts = priceImpacts;
        }

        public GetPriceImpactList(bool status)
        {
            CostUpdateStatus = status;
        }

        public GetPriceImpactList()
        {
            PriceImpacts = new List<GetRMPriceImpact>();
            CostUpdateStatus = false;
            PartsHasMixture = false;
        }

        public List<GetRMPriceImpact> PriceImpacts { get; set; }

        public bool CostUpdateStatus { get; set; }

        public bool PartsHasMixture { get; set; }

    }
}
