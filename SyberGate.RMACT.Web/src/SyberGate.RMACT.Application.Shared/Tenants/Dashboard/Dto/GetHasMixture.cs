using System.Collections.Generic;

namespace SyberGate.RMACT.Tenants.Dashboard.Dto
{
    public class GetHasMixture
    {
        public GetHasMixture(bool hasMixture)
        {
            PartHasMixture = hasMixture;
        }

        public bool PartHasMixture { get; set; }
    }
}
