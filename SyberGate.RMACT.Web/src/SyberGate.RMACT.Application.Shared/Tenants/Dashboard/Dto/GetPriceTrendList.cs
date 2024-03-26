using System.Collections.Generic;

namespace SyberGate.RMACT.Tenants.Dashboard.Dto
{
    public class GetPriceTrendList
    {
        public GetPriceTrendList(List<GetRMPriceTrend> priceTrends)
        {
            PriceTrends = priceTrends;
        }

        public GetPriceTrendList()
        {
            PriceTrends = new List<GetRMPriceTrend>();
        }

        public List<GetRMPriceTrend> PriceTrends { get; set; }
    }
}
