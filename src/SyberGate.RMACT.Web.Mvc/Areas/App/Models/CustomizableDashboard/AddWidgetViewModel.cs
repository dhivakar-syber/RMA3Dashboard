using System.Collections.Generic;
using SyberGate.RMACT.DashboardCustomization.Dto;

namespace SyberGate.RMACT.Web.Areas.App.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
