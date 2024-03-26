using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetRMIndexPriceList
    {
        public GetRMIndexPriceList(List<GetRMIndexPrice> Indexes)
        {
            RMPriceIndex = Indexes;
        }

        public GetRMIndexPriceList()
        {
            RMPriceIndex = new List<GetRMIndexPrice>();
        }

        public List<GetRMIndexPrice> RMPriceIndex{ get; set; }
    }
}
