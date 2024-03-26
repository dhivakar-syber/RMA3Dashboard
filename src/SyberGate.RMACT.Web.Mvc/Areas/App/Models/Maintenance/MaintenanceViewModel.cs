using System.Collections.Generic;
using SyberGate.RMACT.Caching.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}