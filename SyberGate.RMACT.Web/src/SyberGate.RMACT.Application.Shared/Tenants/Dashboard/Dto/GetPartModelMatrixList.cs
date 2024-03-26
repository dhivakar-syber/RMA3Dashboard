using System;
using System.Collections.Generic;
using System.Text;

namespace SyberGate.RMACT.Tenants.Dashboard.Dto
{
    public class GetPartModelMatrixList
    {
        public List<GetPartModelMatrix> PartModelMatrix { get; }

        public GetPartModelMatrixList(List<GetPartModelMatrix> PartModelMatrix)
        {
            PartModelMatrix = PartModelMatrix;
        }


        public GetPartModelMatrixList()
        {
            PartModelMatrix = new List<GetPartModelMatrix>();
            
        }
    }
}
