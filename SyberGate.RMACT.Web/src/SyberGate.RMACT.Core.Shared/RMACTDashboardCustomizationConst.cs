namespace SyberGate.RMACT
{
    public class RMACTDashboardCustomizationConsts
    {
        /// <summary>
        /// Main page name your user will see if they dont change default page's name.
        /// </summary>
        public const string DefaultPageName = "Default Page";

        //Must use underscore instead of dot in widget and filter ids
        //(these data are also used as ids in the input in html pages. Please provide appropriate values.)
        public class Widgets
        {
            public class Tenant
            {
                public const string GeneralStats = "Widgets_Tenant_GeneralStats";
                public const string DailySales = "Widgets_Tenant_DailySales";
                public const string ProfitShare = "Widgets_Tenant_ProfitShare";
                public const string MemberActivity = "Widgets_Tenant_MemberActivity";
                public const string RegionalStats = "Widgets_Tenant_RegionalStats";
                public const string SalesSummary = "Widgets_Tenant_SalesSummary";
                public const string TopStats = "Widgets_Tenant_TopStats";
                public const string RMPriceTrend = "Widgets_Tenant_RMPriceTrend";
                public const string RMPriceHistory = "Widgets_Tenant_RMPriceHistory";
                public const string RMValue = "Widgets_Tenant_RMValue";
                public const string RMTonnage = "Widgets_Tenant_RMTonnage";
                public const string Teams = "Widgets_Tenant_Teams";
                public const string RawMaterials = "Widgets_Tenant_RawMaterials";
                public const string YearlyRMPrice = "Widgets_Tenant_YearlyRMPrice";
                public const string YearlyRMTonnage = "Widgets_Tenant_YearlyRMTonnage";
                public const string AnalysisChart = "Widgets_Tenant_AnalysisChart";
                public const string RMComparison = "Widgets_Tenant_RMComparison";
                public const string AVOBComparison = "Widgets_Tenant_AVOBComparison";
                public const string RMPrice = "WidgetRMPriceStats";
                public const string RMPriceImpact = "Widgets_Tenant_RMPriceImpactStats";
            }

            public class Host
            {
                public const string TopStats = "Widgets_Host_TopStats";
                public const string IncomeStatistics = "Widgets_Host_IncomeStatistics";
                public const string EditionStatistics = "Widgets_Host_EditionStatistics";
                public const string SubscriptionExpiringTenants = "Widgets_Host_SubscriptionExpiringTenants";
                public const string RecentTenants = "Widgets_Host_RecentTenants";
                public const string RMPriceTrend = "Widgets_Host_RMPriceTrend";
                public const string RMPriceHistory = "Widgets_Tenant_RMPriceHistory";
            }
        }

        public class Filters
        {
            public const string FilterDateRangePicker = "Filters_DateRangePicker";
            public const string FilterRMSupplier = "Filters_RMSupplierSelector";
            public const string FilterRMBuyer = "Filters_RMBuyerSelector";
            public const string FilterRMGrade = "Filters_RMGradeSelector";
            public const string FilterRMGroup = "Filters_RMGroupSelector";
            public const string FilterRMPlant = "Filters_RMPlantSelector";
        }

        public class DashboardNames
        {
            public const string DefaultTenantDashboard = "TenantDashboard";

            public const string DefaultHostDashboard = "HostDashboard";

            public const string RMA3Dashboard = "RMA3Dashboard";

            public const string AnalysisDashboard = "AnalysisDashboard";
        }

        public class Applications
        {
            public const string Mvc = "Mvc";
            public const string Angular = "Angular";
        }
    }
}
