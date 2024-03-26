using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using SyberGate.RMACT.Authorization;

namespace SyberGate.RMACT.Web.Areas.App.Startup
{
    public class AppNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Tenant.RMA3Dashboard,
                //        L("A3Dashboard"),
                //        url: "App/RMA3Dashboard",
                //        icon: "flaticon-line-graph",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenant_Dashboard)
                //    )
                //)
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "App/Dashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Dashboard)
                    )
                )
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Common.MedicalSpecialties,
                //        L("MedicalSpecialties"),
                //        url: "App/MedicalSpecialties",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_MedicalSpecialties)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Common.Icd10s,
                //        L("Icd10s"),
                //        url: "App/Icd10s",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Icd10s)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Common.Constants,
                //        L("Constants"),
                //        url: "App/Constants",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Constants)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //    AppPageNames.Host.Tenants,
                //    L("Tenants"),
                //    url: "App/Tenants",
                //    icon: "flaticon-list-3",
                //    permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Host.Editions,
                //        L("Editions"),
                //        url: "App/Editions",
                //        icon: "flaticon-app",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Editions)
                //    )
                //)
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Tenant.Dashboard,
                        L("Dashboard"),
                        url: "App/TenantDashboard",
                        icon: "flaticon-dashboard",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenant_Dashboard)
                    )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Tenant.AnalysisDashboard,
                        L("Analysis"),
                        url: "App/AnalysisDashboard",
                        icon: "flaticon-analytics",
                    permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Analysis_Dashboard)
                    )
                )
                //.AddItem(new MenuItemDefinition(
                //    AppPageNames.Tenant.Analysis,
                //        L("Analysis"),
                //        url: "App/Analysis",
                //        icon: "flaticon-analytics"
                //    //permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Analysis)
                //    )
                //)
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Administration,
                        L("Administration"),
                        icon: "flaticon-interface-8"
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "App/OrganizationUnits",
                            icon: "flaticon-map",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_OrganizationUnits)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.ProcureDatas,
                            L("ProcureDatas"),
                            url: "App/ProcureDatas",
                            icon: "flaticon-web",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_ProcureDatas)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Roles,
                            L("Roles"),
                            url: "App/Roles",
                            icon: "flaticon-suitcase",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Roles)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Users,
                            L("Users"),
                            url: "App/Users",
                            icon: "flaticon-users",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Users)
                        )
                    
                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Common.ApprovalUsers,
                        L("ApprovalUsers"),
                        url: "App/ApprovalUsers",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_ApprovalUsers)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.PartBuckets,
                        L("RMBuckets"),
                        url: "App/PartBuckets",
                        icon: "fa fa-window-restore",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_PartBuckets)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.PartModelMatrixes,
                        L("PartModelMatrixes"),
                        url: "App/PartModelMatrixes",
                        icon: "fa fa-truck",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_PartModelMatrixes)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.LeadModels,
                        L("LeadModels"),
                        url: "App/LeadModels",
                        icon: "fas fa-cogs",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_LeadModels)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Plants,
                        L("Plants"),
                        url: "App/Plants",
                        icon: "fa fa-globe",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Plants)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.RawMaterialMixtures,
                        L("RawMaterialMixtures"),
                        url: "App/RawMaterialMixtures",
                        icon: "flaticon-confetti",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_RawMaterialMixtures)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.RawMaterialIndexes,
                        L("RawMaterialIndexes"),
                        url: "App/RawMaterialIndexes",
                        icon: "flaticon2-indent-dots",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_RawMaterialIndexes)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.IndexNames,
                        L("IndexNames"),
                        url: "App/IndexNames",
                        icon: "flaticon2-crisp-icons",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_IndexNames)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.A3Documents,
                        L("A3Documents"),
                        url: "App/A3Documents",
                        icon: "flaticon-list-3",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_A3Documents)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Years,
                        L("Years"),
                        url: "App/Years",
                        icon: "flaticon-shapes",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Years)
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.RMGroups,
                        L("RMGroups"),
                        url: "App/RMGroups",
                        icon: "flaticon-file-2",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_RMGroups)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Common.UnitOfMeasurements,
                        L("UnitOfMeasurements"),
                        url: "App/UnitOfMeasurements",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_UnitOfMeasurements)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.RawMaterialGrades,
                        L("RawMaterialGrades"),
                        url: "App/RawMaterialGrades",
                        icon: "flaticon-attachment",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_RawMaterialGrades)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.CommodityTrees,
                        L("CommodityTrees"),
                        url: "App/CommodityTrees",
                        icon: "flaticon-map",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_CommodityTrees)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Parts,
                        L("Parts"),
                        url: "App/Parts",
                        icon: "fa fa-wrench",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Parts)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.BaseRMRates,
                        L("BaseRMRates"),
                        url: "App/BaseRMRates",
                        icon: "flaticon-coins",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_BaseRMRates)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Buyers,
                        L("Buyers"),
                        url: "App/Buyers",
                        icon: "flaticon-users",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Buyers)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Suppliers,
                        L("Suppliers"),
                        url: "App/Suppliers",
                        icon: "flaticon-users",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Suppliers)
                    )
                )
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Common.MedicinalTypes,
                //        L("MedicinalTypes"),
                //        url: "App/MedicinalTypes",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_MedicinalTypes)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Common.Gender,
                //        L("Gender"),
                //        url: "App/Gender",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Gender)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Common.Ethnicities,
                //        L("Ethnicities"),
                //        url: "App/Ethnicities",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Ethnicities)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //            AppPageNames.Common.Languages,
                //            L("Languages"),
                //            url: "App/Languages",
                //            icon: "flaticon-tabs",
                //            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Languages)
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //            AppPageNames.Common.AuditLogs,
                //            L("AuditLogs"),
                //            url: "App/AuditLogs",
                //            icon: "flaticon-folder-1",
                //            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_AuditLogs)
                //        )
                //    ).AddItem(new MenuItemDefinition(
                //            AppPageNames.Host.Maintenance,
                //            L("Maintenance"),
                //            url: "App/Maintenance",
                //            icon: "flaticon-lock",
                //            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Maintenance)
                //        )
                //    )
                    //.AddItem(new MenuItemDefinition(
                    //        AppPageNames.Tenant.SubscriptionManagement,
                    //        L("Subscription"),
                    //        url: "App/SubscriptionManagement",
                    //        icon: "flaticon-refresh",
                    //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement)
                    //    )
                    //)
                    //.AddItem(new MenuItemDefinition(
                    //        AppPageNames.Common.UiCustomization,
                    //        L("VisualSettings"),
                    //        url: "App/UiCustomization",
                    //        icon: "flaticon-medical",
                    //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_UiCustomization)
                    //    )
                    //).AddItem(new MenuItemDefinition(
                    //        AppPageNames.Common.WebhookSubscriptions,
                    //        L("WebhookSubscriptions"),
                    //        url: "App/WebhookSubscription",
                    //        icon: "flaticon2-world",
                    //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_WebhookSubscription)
                    //    )
                    //)
                    //.AddItem(new MenuItemDefinition(
                    //        AppPageNames.Common.DynamicProperties,
                    //        L("DynamicProperties"),
                    //        url: "App/DynamicProperty",
                    //        icon: "flaticon-interface-8",
                    //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_DynamicProperties)
                    //    )
                    //)
                    //.AddItem(new MenuItemDefinition(
                    //        AppPageNames.Host.Settings,
                    //        L("Settings"),
                    //        url: "App/HostSettings",
                    //        icon: "flaticon-settings",
                    //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Settings)
                    //    )
                    //)
                    //.AddItem(new MenuItemDefinition(
                    //        AppPageNames.Tenant.Settings,
                    //        L("Settings"),
                    //        url: "App/Settings",
                    //        icon: "flaticon-settings",
                    //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Tenant_Settings)
                    //    )
                    //)
                )
                //.AddItem(new MenuItemDefinition(
                //        AppPageNames.Common.DemoUiComponents,
                //        L("DemoUiComponents"),
                //        url: "App/DemoUiComponents",
                //        icon: "flaticon-shapes",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_DemoUiComponents)
                //    )
                //)
                ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, RMACTConsts.LocalizationSourceName);
        }
    }
}