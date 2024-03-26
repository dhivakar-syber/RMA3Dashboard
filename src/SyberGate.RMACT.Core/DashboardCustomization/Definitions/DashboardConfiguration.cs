using System.Collections.Generic;
using System.Linq;
using Abp.MultiTenancy;
using SyberGate.RMACT.Authorization;

namespace SyberGate.RMACT.DashboardCustomization.Definitions
{
    public class DashboardConfiguration
    {
        public List<DashboardDefinition> DashboardDefinitions { get; } = new List<DashboardDefinition>();

        public List<WidgetDefinition> WidgetDefinitions { get; } = new List<WidgetDefinition>();

        public List<WidgetFilterDefinition> WidgetFilterDefinitions { get; } = new List<WidgetFilterDefinition>();

        public DashboardConfiguration()
        {
            #region FilterDefinitions

            // These are global filter which all widgets can use
            var dateRangeFilter = new WidgetFilterDefinition(
                RMACTDashboardCustomizationConsts.Filters.FilterDateRangePicker,
                "FilterDateRangePicker"
            );

            WidgetFilterDefinitions.Add(dateRangeFilter);

            var rmBuyerFilter = new WidgetFilterDefinition(
               RMACTDashboardCustomizationConsts.Filters.FilterRMBuyer,
               "FilterRMBuyerSelector");

            WidgetFilterDefinitions.Add(rmBuyerFilter);

            var rmSupplierFilter = new WidgetFilterDefinition(
                RMACTDashboardCustomizationConsts.Filters.FilterRMSupplier,
                "FilterRMSupplierSelector");

            WidgetFilterDefinitions.Add(rmSupplierFilter);

            var rmGradeFilter = new WidgetFilterDefinition(
               RMACTDashboardCustomizationConsts.Filters.FilterRMGrade,
               "FilterRMGradeSelector");

            WidgetFilterDefinitions.Add(rmGradeFilter);


            var rmGroupFilter = new WidgetFilterDefinition(
                RMACTDashboardCustomizationConsts.Filters.FilterRMGroup,
                "FilterRMGroupSelector");

            WidgetFilterDefinitions.Add(rmGroupFilter);

            var rmPlantFilter = new WidgetFilterDefinition(
               RMACTDashboardCustomizationConsts.Filters.FilterRMPlant,
               "FilterRMPlantSelector");

            WidgetFilterDefinitions.Add(rmPlantFilter);
            // Add your filters here

            #endregion

            #region WidgetDefinitions

            // Define Widgets

            #region TenantWidgets

            var tenantWidgetsDefaultPermission = new List<string>
            {
                AppPermissions.Pages_Tenant_Dashboard
            };

            var dailySales = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.DailySales,
                "WidgetDailySales",
                side: MultiTenancySides.Tenant,
                usedWidgetFilters: new List<string> { dateRangeFilter.Id },
                permissions: tenantWidgetsDefaultPermission
            );

            var generalStats = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.GeneralStats,
                "WidgetGeneralStats",
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission.Concat(new List<string>{ AppPermissions.Pages_Administration_AuditLogs }).ToList());

            var profitShare = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.ProfitShare,
                "WidgetProfitShare",
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var memberActivity = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.MemberActivity,
                "WidgetMemberActivity",
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var regionalStats = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RegionalStats,
                "WidgetRegionalStats",
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var salesSummary = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.SalesSummary,
                "WidgetSalesSummary",
                usedWidgetFilters: new List<string>() { dateRangeFilter.Id },
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var topStats = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.TopStats,
                "WidgetTopStats",
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var rmPriceTrend = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceTrend,
                "WidgetRMPriceTrend",
                usedWidgetFilters: new List<string> { dateRangeFilter.Id, rmSupplierFilter.Id, rmBuyerFilter.Id,rmGradeFilter.Id, rmGroupFilter.Id, rmPlantFilter.Id },
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var rmPriceImpact = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceImpact,
                "WidgetRMPriceImpactStats",
                usedWidgetFilters: new List<string> { dateRangeFilter.Id, rmSupplierFilter.Id, rmBuyerFilter.Id, rmGradeFilter.Id, rmGroupFilter.Id, rmPlantFilter.Id },
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var rmPriceHistory = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPriceHistory,
                "WidgetRMPriceHistory",
                usedWidgetFilters: new List<string> { rmSupplierFilter.Id, rmBuyerFilter.Id, rmGradeFilter.Id, rmGroupFilter.Id },
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            var rmPrice = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPrice,
                "WidgetRMPriceStats",
                //usedWidgetFilters: new List<string> { dateRangeFilter.Id, rmSupplierFilter.Id, rmBuyerFilter.Id, rmGradeFilter.Id, rmGroupFilter.Id, rmPlantFilter.Id },
                side: MultiTenancySides.Tenant,
                permissions: tenantWidgetsDefaultPermission);

            //WidgetDefinitions.Add(generalStats);
            //WidgetDefinitions.Add(dailySales);
            //WidgetDefinitions.Add(profitShare);
            //WidgetDefinitions.Add(memberActivity);
            //WidgetDefinitions.Add(regionalStats);
            //WidgetDefinitions.Add(topStats);
            //WidgetDefinitions.Add(salesSummary);
            WidgetDefinitions.Add(rmPriceTrend);
            WidgetDefinitions.Add(rmPriceImpact);
            WidgetDefinitions.Add(rmPriceHistory);
            WidgetDefinitions.Add(rmPrice);
            // Add your tenant side widgets here

            #endregion

            #region HostWidgets

            var hostWidgetsDefaultPermission = new List<string>
            {
                AppPermissions.Pages_Administration_Host_Dashboard
            };

            var incomeStatistics = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Host.IncomeStatistics,
                "WidgetIncomeStatistics",
                side: MultiTenancySides.Host,
                permissions: hostWidgetsDefaultPermission);

            var hostTopStats = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Host.TopStats,
                "WidgetTopStats",
                side: MultiTenancySides.Host,
                permissions: hostWidgetsDefaultPermission);

            var editionStatistics = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Host.EditionStatistics,
                "WidgetEditionStatistics",
                side: MultiTenancySides.Host,
                permissions: hostWidgetsDefaultPermission);

            var subscriptionExpiringTenants = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Host.SubscriptionExpiringTenants,
                "WidgetSubscriptionExpiringTenants",
                side: MultiTenancySides.Host,
                permissions: hostWidgetsDefaultPermission);

            var recentTenants = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Host.RecentTenants,
                "WidgetRecentTenants",
                side: MultiTenancySides.Host,
                usedWidgetFilters: new List<string>() { dateRangeFilter.Id },
                permissions: hostWidgetsDefaultPermission);

            WidgetDefinitions.Add(incomeStatistics);
            WidgetDefinitions.Add(hostTopStats);
            WidgetDefinitions.Add(editionStatistics);
            WidgetDefinitions.Add(subscriptionExpiringTenants);
            WidgetDefinitions.Add(recentTenants);

            // Add your host side widgets here

            #endregion
            #region AnalysisWidgets

            var analysisWidgetsDefaultPermission = new List<string>
            {
                AppPermissions.Pages_Analysis_Dashboard
            };
            
            var dailySales1 = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.DailySales,
                "WidgetDailySales",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var generalStats1 = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.GeneralStats,
                "WidgetGeneralStats",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission.Concat(new List<string> { AppPermissions.Pages_Administration_AuditLogs }).ToList());

            var rmValue = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMValue,
                "WidgetRMValue",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var rmTonnage = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMTonnage,
                "WidgetRMTonnage",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var teams = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.Teams,
                "WidgetTeams",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var rawMaterials = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RawMaterials,
                "WidgetRawMaterials",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var yearlyRMPrice = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.YearlyRMPrice,
                "WidgetYearlyRMPrice",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var yearlyRMTonnage = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.YearlyRMTonnage,
                "WidgetYearlyRMTonnage",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var analysisChart = new WidgetDefinition(
               RMACTDashboardCustomizationConsts.Widgets.Tenant.AnalysisChart,
               "WidgetAnalysisChart",
               side: MultiTenancySides.Tenant,
               permissions: analysisWidgetsDefaultPermission);

            var rMComparison = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMComparison,
                "WidgetRMComparison",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var aVOBComparison = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.AVOBComparison,
                "WidgetAVOBComparison",
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            var rmPricestats = new WidgetDefinition(
                RMACTDashboardCustomizationConsts.Widgets.Tenant.RMPrice,
                "WidgetRMPriceStats",
                //usedWidgetFilters: new List<string> { dateRangeFilter.Id, rmSupplierFilter.Id, rmBuyerFilter.Id, rmGradeFilter.Id, rmGroupFilter.Id, rmPlantFilter.Id },
                side: MultiTenancySides.Tenant,
                permissions: analysisWidgetsDefaultPermission);

            //WidgetDefinitions.Add(rmPriceTrend1);
            WidgetDefinitions.Add(dailySales1);
            WidgetDefinitions.Add(rmValue);
            WidgetDefinitions.Add(rmTonnage);
            WidgetDefinitions.Add(teams);
            WidgetDefinitions.Add(rmPricestats);
            WidgetDefinitions.Add(generalStats1);
            WidgetDefinitions.Add(rawMaterials);
            WidgetDefinitions.Add(yearlyRMPrice);
            WidgetDefinitions.Add(yearlyRMTonnage);
            WidgetDefinitions.Add(rMComparison);
            WidgetDefinitions.Add(analysisChart);
            WidgetDefinitions.Add(aVOBComparison);


            #endregion
            #endregion

            #region DashboardDefinitions

            var rma3Dashboard = new DashboardDefinition(
                RMACTDashboardCustomizationConsts.DashboardNames.RMA3Dashboard,
                new List<string>
                {
                    generalStats.Id, dailySales.Id, profitShare.Id, memberActivity.Id, regionalStats.Id, topStats.Id, salesSummary.Id, rmPriceTrend.Id, rmPriceImpact.Id,
                    rmPriceTrend.Id, rmPriceImpact.Id, rmPriceHistory.Id,rmValue.Id,rmTonnage.Id,rmPrice.Id
                });
            DashboardDefinitions.Add(rma3Dashboard);

            // Create dashboard
            var defaultTenantDashboard = new DashboardDefinition(
                RMACTDashboardCustomizationConsts.DashboardNames.DefaultTenantDashboard,
                new List<string>
                {
                    //generalStats.Id, dailySales.Id, profitShare.Id, memberActivity.Id, regionalStats.Id, topStats.Id, salesSummary.Id, rmPriceTrend.Id, rmPriceImpact.Id
                    rmPriceTrend.Id, rmPriceImpact.Id,rmPrice.Id
                });

            DashboardDefinitions.Add(defaultTenantDashboard);

            var defaultHostDashboard = new DashboardDefinition(
                RMACTDashboardCustomizationConsts.DashboardNames.DefaultHostDashboard,
                new List<string>
                {
                    incomeStatistics.Id,
                    hostTopStats.Id,
                    editionStatistics.Id,
                    subscriptionExpiringTenants.Id,
                    recentTenants.Id
                });

            DashboardDefinitions.Add(defaultHostDashboard);

            // Add your dashboard definiton here

            var analysisDashboard = new DashboardDefinition(
               RMACTDashboardCustomizationConsts.DashboardNames.AnalysisDashboard,
               new List<string>
               {
                    rmValue.Id,rmTonnage.Id,teams.Id,rawMaterials.Id,yearlyRMPrice.Id,yearlyRMTonnage.Id,rMComparison.Id,analysisChart.Id,aVOBComparison.Id,rmPricestats.Id

               });
            DashboardDefinitions.Add(analysisDashboard);
            #endregion

        }

    }
}
