using System.Collections.Generic;
using SyberGate.RMACT.Web.DashboardCustomization;


namespace SyberGate.RMACT.Web.Areas.App.Startup
{
    public class DashboardViewConfiguration
    {
        public Dictionary<string, WidgetViewDefinition> WidgetViewDefinitions { get; } = new Dictionary<string, WidgetViewDefinition>();

        public Dictionary<string, WidgetFilterViewDefinition> WidgetFilterViewDefinitions { get; } = new Dictionary<string, WidgetFilterViewDefinition>();

        public DashboardViewConfiguration()
        {
            var jsAndCssFileRoot = "/Areas/App/Views/CustomizableDashboard/Widgets/";
            var viewFileRoot = "~/Areas/App/Views/Shared/Components/CustomizableDashboard/Widgets/";

            #region FilterViewDefinitions

            WidgetFilterViewDefinitions.Add(RMACTDashboardCustomizationConsts.Filters.FilterDateRangePicker,
                new WidgetFilterViewDefinition(
                    RMACTDashboardCustomizationConsts.Filters.FilterDateRangePicker,
                    viewFileRoot + "DateRangeFilter.cshtml",
                    jsAndCssFileRoot + "DateRangeFilter/DateRangeFilter.min.js",
                    jsAndCssFileRoot + "DateRangeFilter/DateRangeFilter.min.css")
            );

            WidgetFilterViewDefinitions.Add(RMACTDashboardCustomizationConsts.Filters.FilterRMSupplier,
                new WidgetFilterViewDefinition(
                    RMACTDashboardCustomizationConsts.Filters.FilterRMSupplier,
                    viewFileRoot + "RMSupplierFilter.cshtml",
                    jsAndCssFileRoot + "RMSupplierFilter/RMSupplierFilter.min.js",
                    jsAndCssFileRoot + "RMSupplierFilter/RMSupplierFilter.min.css")
            );

            WidgetFilterViewDefinitions.Add(RMACTDashboardCustomizationConsts.Filters.FilterRMBuyer,
                new WidgetFilterViewDefinition(
                    RMACTDashboardCustomizationConsts.Filters.FilterRMBuyer,
                    viewFileRoot + "RMBuyerFilter.cshtml",
                    jsAndCssFileRoot + "RMBuyerFilter/RMBuyerFilter.min.js",
                    jsAndCssFileRoot + "RMBuyerFilter/RMBuyerFilter.min.css")
            );

            

            WidgetFilterViewDefinitions.Add(RMACTDashboardCustomizationConsts.Filters.FilterRMGroup,
                new WidgetFilterViewDefinition(
                    RMACTDashboardCustomizationConsts.Filters.FilterRMGroup,
                    viewFileRoot + "RMGroupFilter.cshtml",
                    jsAndCssFileRoot + "RMGroupFilter/RMGroupFilter.min.js",
                    jsAndCssFileRoot + "RMGroupFilter/RMGroupFilter.min.css")
            );

            WidgetFilterViewDefinitions.Add(RMACTDashboardCustomizationConsts.Filters.FilterRMPlant,
                new WidgetFilterViewDefinition(
                    RMACTDashboardCustomizationConsts.Filters.FilterRMPlant,
                    viewFileRoot + "RMPlantFilter.cshtml",
                    jsAndCssFileRoot + "RMPlantFilter/RMPlantFilter.min.js",
                    jsAndCssFileRoot + "RMPlantFilter/RMPlantFilter.min.css")
            );

            WidgetFilterViewDefinitions.Add(RMACTDashboardCustomizationConsts.Filters.FilterRMGrade,
                new WidgetFilterViewDefinition(
                    RMACTDashboardCustomizationConsts.Filters.FilterRMGrade,
                    viewFileRoot + "RMGradeFilter.cshtml",
                    jsAndCssFileRoot + "RMGradeFilter/RMGradeFilter.min.js",
                    jsAndCssFileRoot + "RMGradeFilter/RMGradeFilter.min.css")
            );
            //add your filters iew definitions here
            #endregion

            #region WidgetViewDefinitions

            #region TenantWidgets

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceImpact,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceImpact,
                    viewFileRoot + "RMPriceImpact.cshtml",
                    jsAndCssFileRoot + "RMPriceImpact/RMPriceImpact.min.js",
                    jsAndCssFileRoot + "RMPriceImpact/RMPriceImpact.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceTrend,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceTrend,
                    viewFileRoot + "RMPriceTrend.cshtml",
                    jsAndCssFileRoot + "RMPriceTrend/RMPriceTrend.min.js",
                    jsAndCssFileRoot + "RMPriceTrend/RMPriceTrend.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceHistory,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceHistory,
                    viewFileRoot + "RMPriceHistory.cshtml",
                    jsAndCssFileRoot + "RMPriceHistory/RMPriceHistory.min.js",
                    jsAndCssFileRoot + "RMPriceHistory/RMPriceHistory.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RMValue,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.RMValue,
                    viewFileRoot + "RMValue.cshtml",
                    jsAndCssFileRoot + "RMValue/RMValue.min.js",
                    jsAndCssFileRoot + "RMValue/RMValue.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RMTonnage,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.RMTonnage,
                    viewFileRoot + "RMTonnage.cshtml",
                    jsAndCssFileRoot + "RMTonnage/RMTonnage.min.js",
                    jsAndCssFileRoot + "RMTonnage/RMTonnage.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.Teams,
               new WidgetViewDefinition(
                   RMACTDashboardCustomizationConsts.Widgets.Tenant.Teams,
                   viewFileRoot + "Teams.cshtml",
                   jsAndCssFileRoot + "Teams/Teams.min.js",
                   jsAndCssFileRoot + "Teams/Teams.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RawMaterials,
               new WidgetViewDefinition(
                   RMACTDashboardCustomizationConsts.Widgets.Tenant.RawMaterials,
                   viewFileRoot + "RawMaterials.cshtml",
                   jsAndCssFileRoot + "RawMaterials/RawMaterials.min.js",
                   jsAndCssFileRoot + "RawMaterials/RawMaterials.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.YearlyRMPrice,
              new WidgetViewDefinition(
                  RMACTDashboardCustomizationConsts.Widgets.Tenant.YearlyRMPrice,
                  viewFileRoot + "YearlyRMPrice.cshtml",
                  jsAndCssFileRoot + "YearlyRMPrice/YearlyRMPrice.min.js",
                  jsAndCssFileRoot + "YearlyRMPrice/YearlyRMPrice.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.AnalysisChart,
             new WidgetViewDefinition(
                 RMACTDashboardCustomizationConsts.Widgets.Tenant.AnalysisChart,
                 viewFileRoot + "AnalysisChart.cshtml",
                 jsAndCssFileRoot + "AnalysisChart/AnalysisChart.min.js",
                 jsAndCssFileRoot + "AnalysisChart/AnalysisChart.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.YearlyRMTonnage,
              new WidgetViewDefinition(
                  RMACTDashboardCustomizationConsts.Widgets.Tenant.YearlyRMTonnage,
                  viewFileRoot + "YearlyRMTonnage.cshtml",
                  jsAndCssFileRoot + "YearlyRMTonnage/YearlyRMTonnage.min.js",
                  jsAndCssFileRoot + "YearlyRMTonnage/YearlyRMTonnage.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RMComparison,
              new WidgetViewDefinition(
                  RMACTDashboardCustomizationConsts.Widgets.Tenant.RMComparison,
                  viewFileRoot + "RMComparison.cshtml",
                  jsAndCssFileRoot + "RMComparison/RMComparison.min.js",
                  jsAndCssFileRoot + "RMComparison/RMComparison.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.AVOBComparison,
              new WidgetViewDefinition(
                  RMACTDashboardCustomizationConsts.Widgets.Tenant.AVOBComparison,
                  viewFileRoot + "AVOBComparison.cshtml",
                  jsAndCssFileRoot + "AVOBComparison/AVOBComparison.min.js",
                  jsAndCssFileRoot + "AVOBComparison/AVOBComparison.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPrice,
               new WidgetViewDefinition(
                   RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPrice,
                   viewFileRoot + "RMPrice.cshtml",
                   jsAndCssFileRoot + "RMPrice/RMPrice.min.js",
                   jsAndCssFileRoot + "RMPrice/RMPrice.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.DailySales,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.DailySales,
                    viewFileRoot + "DailySales.cshtml",
                    jsAndCssFileRoot + "DailySales/DailySales.min.js",
                    jsAndCssFileRoot + "DailySales/DailySales.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.GeneralStats,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.GeneralStats,
                    viewFileRoot + "GeneralStats.cshtml",
                    jsAndCssFileRoot + "GeneralStats/GeneralStats.min.js",
                    jsAndCssFileRoot + "GeneralStats/GeneralStats.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.ProfitShare,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.ProfitShare,
                    viewFileRoot + "ProfitShare.cshtml",
                    jsAndCssFileRoot + "ProfitShare/ProfitShare.min.js",
                    jsAndCssFileRoot + "ProfitShare/ProfitShare.min.css"));
  
            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.MemberActivity,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.MemberActivity,
                    viewFileRoot + "MemberActivity.cshtml",
                    jsAndCssFileRoot + "MemberActivity/MemberActivity.min.js",
                    jsAndCssFileRoot + "MemberActivity/MemberActivity.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.RegionalStats,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.RegionalStats,
                    viewFileRoot + "RegionalStats.cshtml",
                    jsAndCssFileRoot + "RegionalStats/RegionalStats.min.js",
                    jsAndCssFileRoot + "RegionalStats/RegionalStats.min.css",
                    12,
                    10));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.SalesSummary,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.SalesSummary,
                    viewFileRoot + "SalesSummary.cshtml",
                    jsAndCssFileRoot + "SalesSummary/SalesSummary.min.js",
                    jsAndCssFileRoot + "SalesSummary/SalesSummary.min.css",
                    6,
                    10));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Tenant.TopStats,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Tenant.TopStats,
                    viewFileRoot + "TopStats.cshtml",
                    jsAndCssFileRoot + "TopStats/TopStats.min.js",
                    jsAndCssFileRoot + "TopStats/TopStats.min.css",
                    12,
                    10));

            //add your tenant side widget definitions here
            #endregion

            #region HostWidgets

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Host.IncomeStatistics,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Host.IncomeStatistics,
                    viewFileRoot + "IncomeStatistics.cshtml",
                    jsAndCssFileRoot + "IncomeStatistics/IncomeStatistics.min.js",
                    jsAndCssFileRoot + "IncomeStatistics/IncomeStatistics.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Host.TopStats,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Host.TopStats,
                    viewFileRoot + "HostTopStats.cshtml",
                    jsAndCssFileRoot + "HostTopStats/HostTopStats.min.js",
                    jsAndCssFileRoot + "HostTopStats/HostTopStats.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Host.EditionStatistics,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Host.EditionStatistics,
                    viewFileRoot + "EditionStatistics.cshtml",
                    jsAndCssFileRoot + "EditionStatistics/EditionStatistics.min.js",
                    jsAndCssFileRoot + "EditionStatistics/EditionStatistics.min.css"));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Host.SubscriptionExpiringTenants,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Host.SubscriptionExpiringTenants,
                    viewFileRoot + "SubscriptionExpiringTenants.cshtml",
                    jsAndCssFileRoot + "SubscriptionExpiringTenants/SubscriptionExpiringTenants.min.js",
                    jsAndCssFileRoot + "SubscriptionExpiringTenants/SubscriptionExpiringTenants.min.css",
                    6,
                    10));

            WidgetViewDefinitions.Add(RMACTDashboardCustomizationConsts.Widgets.Host.RecentTenants,
                new WidgetViewDefinition(
                    RMACTDashboardCustomizationConsts.Widgets.Host.RecentTenants,
                    viewFileRoot + "RecentTenants.cshtml",
                    jsAndCssFileRoot + "RecentTenants/RecentTenants.min.js",
                    jsAndCssFileRoot + "RecentTenants/RecentTenants.min.css"));

            //add your host side widgets definitions here
            #endregion

            #endregion
        }
    }
}
