using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Masters.Dtos
{
    public class GetPartBucketForProcessViewDto
    {
        public decimal Price { get; set;  }
        public decimal BasePrice { get; set; }

        public List<PartBucketDto> PartBucketDetails { get; set; }
    }
}
