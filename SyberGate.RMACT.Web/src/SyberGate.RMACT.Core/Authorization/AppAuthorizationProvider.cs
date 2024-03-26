using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SyberGate.RMACT.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var procureDatas = pages.CreateChildPermission(AppPermissions.Pages_Administration_ProcureDatas, L("ProcureDatas"));
            procureDatas.CreateChildPermission(AppPermissions.Pages_Administration_ProcureDatas_Create, L("CreateNewProcureData"));
            procureDatas.CreateChildPermission(AppPermissions.Pages_Administration_ProcureDatas_Edit, L("EditProcureData"));
            procureDatas.CreateChildPermission(AppPermissions.Pages_Administration_ProcureDatas_Delete, L("DeleteProcureData"));



            var commodityTrees = pages.CreateChildPermission(AppPermissions.Pages_CommodityTrees, L("CommodityTrees"));
            commodityTrees.CreateChildPermission(AppPermissions.Pages_CommodityTrees_ManageCommodityTree, L("ManagingCommodityTree"));
            commodityTrees.CreateChildPermission(AppPermissions.Pages_CommodityTrees_Create, L("CreateNewCommodityTree"));
            commodityTrees.CreateChildPermission(AppPermissions.Pages_CommodityTrees_Edit, L("EditCommodityTree"));
            commodityTrees.CreateChildPermission(AppPermissions.Pages_CommodityTrees_Delete, L("DeleteCommodityTree"));



            var rmGroups = pages.CreateChildPermission(AppPermissions.Pages_Administration_RMGroups, L("RMGroups"));
            rmGroups.CreateChildPermission(AppPermissions.Pages_Administration_RMGroups_Create, L("CreateNewRMGroup"));
            rmGroups.CreateChildPermission(AppPermissions.Pages_Administration_RMGroups_Edit, L("EditRMGroup"));
            rmGroups.CreateChildPermission(AppPermissions.Pages_Administration_RMGroups_Delete, L("DeleteRMGroup"));



     
            //var medicalSpecialties = pages.CreateChildPermission(AppPermissions.Pages_MedicalSpecialties, L("MedicalSpecialties"));
            //medicalSpecialties.CreateChildPermission(AppPermissions.Pages_MedicalSpecialties_Create, L("CreateNewMedicalSpecialty"));
            //medicalSpecialties.CreateChildPermission(AppPermissions.Pages_MedicalSpecialties_Edit, L("EditMedicalSpecialty"));
            //medicalSpecialties.CreateChildPermission(AppPermissions.Pages_MedicalSpecialties_Delete, L("DeleteMedicalSpecialty"));

            //var icd10s = pages.CreateChildPermission(AppPermissions.Pages_Icd10s, L("Icd10s"));
            //icd10s.CreateChildPermission(AppPermissions.Pages_Icd10s_Create, L("CreateNewIcd10"));
            //icd10s.CreateChildPermission(AppPermissions.Pages_Icd10s_Edit, L("EditIcd10"));
            //icd10s.CreateChildPermission(AppPermissions.Pages_Icd10s_Delete, L("DeleteIcd10"));

            //var constants = pages.CreateChildPermission(AppPermissions.Pages_Constants, L("Constants"));
            //constants.CreateChildPermission(AppPermissions.Pages_Constants_Create, L("CreateNewConstant"));
            //constants.CreateChildPermission(AppPermissions.Pages_Constants_Edit, L("EditConstant"));
            //constants.CreateChildPermission(AppPermissions.Pages_Constants_Delete, L("DeleteConstant"));

            //var gender = pages.CreateChildPermission(AppPermissions.Pages_Administration_Gender, L("Gender"));
            //gender.CreateChildPermission(AppPermissions.Pages_Administration_Gender_Create, L("CreateNewGender"));
            //gender.CreateChildPermission(AppPermissions.Pages_Administration_Gender_Edit, L("EditGender"));
            //gender.CreateChildPermission(AppPermissions.Pages_Administration_Gender_Delete, L("DeleteGender"));

            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var approvalUsers = administration.CreateChildPermission(AppPermissions.Pages_Administration_ApprovalUsers, L("ApprovalUsers"));
            approvalUsers.CreateChildPermission(AppPermissions.Pages_Administration_ApprovalUsers_Create, L("CreateNewApprovalUser"));
            approvalUsers.CreateChildPermission(AppPermissions.Pages_Administration_ApprovalUsers_Edit, L("EditApprovalUser"));
            approvalUsers.CreateChildPermission(AppPermissions.Pages_Administration_ApprovalUsers_Delete, L("DeleteApprovalUser"));



            var partBuckets = administration.CreateChildPermission(AppPermissions.Pages_Administration_PartBuckets, L("PartBuckets"));
            partBuckets.CreateChildPermission(AppPermissions.Pages_Administration_PartBuckets_Create, L("CreateNewPartBucket"));
            partBuckets.CreateChildPermission(AppPermissions.Pages_Administration_PartBuckets_Edit, L("EditPartBucket"));
            partBuckets.CreateChildPermission(AppPermissions.Pages_Administration_PartBuckets_Delete, L("DeletePartBucket"));



            var partModelMatrixes = administration.CreateChildPermission(AppPermissions.Pages_Administration_PartModelMatrixes, L("PartModelMatrixes"));
            partModelMatrixes.CreateChildPermission(AppPermissions.Pages_Administration_PartModelMatrixes_Create, L("CreateNewPartModelMatrix"));
            partModelMatrixes.CreateChildPermission(AppPermissions.Pages_Administration_PartModelMatrixes_Edit, L("EditPartModelMatrix"));
            partModelMatrixes.CreateChildPermission(AppPermissions.Pages_Administration_PartModelMatrixes_Delete, L("DeletePartModelMatrix"));



            var leadModels = administration.CreateChildPermission(AppPermissions.Pages_Administration_LeadModels, L("LeadModels"));
            leadModels.CreateChildPermission(AppPermissions.Pages_Administration_LeadModels_Create, L("CreateNewLeadModel"));
            leadModels.CreateChildPermission(AppPermissions.Pages_Administration_LeadModels_Edit, L("EditLeadModel"));
            leadModels.CreateChildPermission(AppPermissions.Pages_Administration_LeadModels_Delete, L("DeleteLeadModel"));



            var plants = administration.CreateChildPermission(AppPermissions.Pages_Administration_Plants, L("Plants"));
            plants.CreateChildPermission(AppPermissions.Pages_Administration_Plants_Create, L("CreateNewPlant"));
            plants.CreateChildPermission(AppPermissions.Pages_Administration_Plants_Edit, L("EditPlant"));
            plants.CreateChildPermission(AppPermissions.Pages_Administration_Plants_Delete, L("DeletePlant"));





            var rawMaterialMixtures = administration.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialMixtures, L("RawMaterialMixtures"));
            rawMaterialMixtures.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialMixtures_Create, L("CreateNewRawMaterialMixture"));
            rawMaterialMixtures.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialMixtures_Edit, L("EditRawMaterialMixture"));
            rawMaterialMixtures.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialMixtures_Delete, L("DeleteRawMaterialMixture"));



            var rawMaterialIndexes = administration.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialIndexes, L("RawMaterialIndexes"));
            rawMaterialIndexes.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialIndexes_Create, L("CreateNewRawMaterialIndex"));
            rawMaterialIndexes.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialIndexes_Edit, L("EditRawMaterialIndex"));
            rawMaterialIndexes.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialIndexes_Delete, L("DeleteRawMaterialIndex"));



            var indexNames = administration.CreateChildPermission(AppPermissions.Pages_Administration_IndexNames, L("IndexNames"));
            indexNames.CreateChildPermission(AppPermissions.Pages_Administration_IndexNames_Create, L("CreateNewIndexName"));
            indexNames.CreateChildPermission(AppPermissions.Pages_Administration_IndexNames_Edit, L("EditIndexName"));
            indexNames.CreateChildPermission(AppPermissions.Pages_Administration_IndexNames_Delete, L("DeleteIndexName"));



            var a3Documents = administration.CreateChildPermission(AppPermissions.Pages_Administration_A3Documents, L("A3Documents"));
            a3Documents.CreateChildPermission(AppPermissions.Pages_Administration_A3Documents_Create, L("CreateNewA3Document"));
            a3Documents.CreateChildPermission(AppPermissions.Pages_Administration_A3Documents_Edit, L("EditA3Document"));
            a3Documents.CreateChildPermission(AppPermissions.Pages_Administration_A3Documents_Delete, L("DeleteA3Document"));
            a3Documents.CreateChildPermission(AppPermissions.Pages_Administration_A3Documents_Confirm, L("ConfirmA3Document"));



            var years = administration.CreateChildPermission(AppPermissions.Pages_Administration_Years, L("Years"));
            years.CreateChildPermission(AppPermissions.Pages_Administration_Years_Create, L("CreateNewYear"));
            years.CreateChildPermission(AppPermissions.Pages_Administration_Years_Edit, L("EditYear"));
            years.CreateChildPermission(AppPermissions.Pages_Administration_Years_Delete, L("DeleteYear"));



            var unitOfMeasurements = administration.CreateChildPermission(AppPermissions.Pages_Administration_UnitOfMeasurements, L("UnitOfMeasurements"));
            unitOfMeasurements.CreateChildPermission(AppPermissions.Pages_Administration_UnitOfMeasurements_Create, L("CreateNewUnitOfMeasurement"));
            unitOfMeasurements.CreateChildPermission(AppPermissions.Pages_Administration_UnitOfMeasurements_Edit, L("EditUnitOfMeasurement"));
            unitOfMeasurements.CreateChildPermission(AppPermissions.Pages_Administration_UnitOfMeasurements_Delete, L("DeleteUnitOfMeasurement"));



            var rawMaterialGrades = administration.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialGrades, L("RawMaterialGrades"));
            rawMaterialGrades.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialGrades_Create, L("CreateNewRawMaterialGrade"));
            rawMaterialGrades.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialGrades_Edit, L("EditRawMaterialGrade"));
            rawMaterialGrades.CreateChildPermission(AppPermissions.Pages_Administration_RawMaterialGrades_Delete, L("DeleteRawMaterialGrade"));



            var parts = administration.CreateChildPermission(AppPermissions.Pages_Administration_Parts, L("Parts"));
            parts.CreateChildPermission(AppPermissions.Pages_Administration_Parts_Create, L("CreateNewPart"));
            parts.CreateChildPermission(AppPermissions.Pages_Administration_Parts_Edit, L("EditPart"));
            parts.CreateChildPermission(AppPermissions.Pages_Administration_Parts_Delete, L("DeletePart"));



            var baseRMRates = administration.CreateChildPermission(AppPermissions.Pages_Administration_BaseRMRates, L("BaseRMRates"));
            baseRMRates.CreateChildPermission(AppPermissions.Pages_Administration_BaseRMRates_Create, L("CreateNewBaseRMRate"));
            baseRMRates.CreateChildPermission(AppPermissions.Pages_Administration_BaseRMRates_Edit, L("EditBaseRMRate"));
            baseRMRates.CreateChildPermission(AppPermissions.Pages_Administration_BaseRMRates_Delete, L("DeleteBaseRMRate"));



            var uoMs = administration.CreateChildPermission(AppPermissions.Pages_Administration_UOMs, L("UOMs"));
            uoMs.CreateChildPermission(AppPermissions.Pages_Administration_UOMs_Create, L("CreateNewUOM"));
            uoMs.CreateChildPermission(AppPermissions.Pages_Administration_UOMs_Edit, L("EditUOM"));
            uoMs.CreateChildPermission(AppPermissions.Pages_Administration_UOMs_Delete, L("DeleteUOM"));



            //         var rmGrades = administration.CreateChildPermission(AppPermissions.Pages_Administration_RMGrades, L("RMGrades"));
            //rmGrades.CreateChildPermission(AppPermissions.Pages_Administration_RMGrades_Create, L("CreateNewRMGrade"));
            //rmGrades.CreateChildPermission(AppPermissions.Pages_Administration_RMGrades_Edit, L("EditRMGrade"));
            //rmGrades.CreateChildPermission(AppPermissions.Pages_Administration_RMGrades_Delete, L("DeleteRMGrade"));



            var buyers = administration.CreateChildPermission(AppPermissions.Pages_Administration_Buyers, L("Buyers"));
            buyers.CreateChildPermission(AppPermissions.Pages_Administration_Buyers_Create, L("CreateNewBuyer"));
            buyers.CreateChildPermission(AppPermissions.Pages_Administration_Buyers_Edit, L("EditBuyer"));
            buyers.CreateChildPermission(AppPermissions.Pages_Administration_Buyers_Delete, L("DeleteBuyer"));



            var suppliers = administration.CreateChildPermission(AppPermissions.Pages_Administration_Suppliers, L("Suppliers"));
            suppliers.CreateChildPermission(AppPermissions.Pages_Administration_Suppliers_Create, L("CreateNewSupplier"));
            suppliers.CreateChildPermission(AppPermissions.Pages_Administration_Suppliers_Edit, L("EditSupplier"));
            suppliers.CreateChildPermission(AppPermissions.Pages_Administration_Suppliers_Delete, L("DeleteSupplier"));



            //var medicinalTypes = administration.CreateChildPermission(AppPermissions.Pages_Administration_MedicinalTypes, L("MedicinalTypes"));
            //medicinalTypes.CreateChildPermission(AppPermissions.Pages_Administration_MedicinalTypes_Create, L("CreateNewMedicinalType"));
            //medicinalTypes.CreateChildPermission(AppPermissions.Pages_Administration_MedicinalTypes_Edit, L("EditMedicinalType"));
            //medicinalTypes.CreateChildPermission(AppPermissions.Pages_Administration_MedicinalTypes_Delete, L("DeleteMedicinalType"));

            //var ethnicities = administration.CreateChildPermission(AppPermissions.Pages_Administration_Ethnicities, L("Ethnicities"));
            //ethnicities.CreateChildPermission(AppPermissions.Pages_Administration_Ethnicities_Create, L("CreateNewEthnicity"));
            //ethnicities.CreateChildPermission(AppPermissions.Pages_Administration_Ethnicities_Edit, L("EditEthnicity"));
            //ethnicities.CreateChildPermission(AppPermissions.Pages_Administration_Ethnicities_Delete, L("DeleteEthnicity"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
            //webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
            //webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
            //webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
            //webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
            //webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
            //webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

            //var dynamicProperties = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties, L("DynamicProperties"));
            //dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Create, L("CreatingDynamicProperties"));
            //dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Edit, L("EditingDynamicProperties"));
            //dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Delete, L("DeletingDynamicProperties"));

            //var dynamicPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue, L("DynamicPropertyValue"));
            //dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Create, L("CreatingDynamicPropertyValue"));
            //dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Edit, L("EditingDynamicPropertyValue"));
            //dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Delete, L("DeletingDynamicPropertyValue"));

            //var dynamicEntityProperties = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties, L("DynamicEntityProperties"));
            //dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Create, L("CreatingDynamicEntityProperties"));
            //dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Edit, L("EditingDynamicEntityProperties"));
            //dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Delete, L("DeletingDynamicEntityProperties"));

            //var dynamicEntityPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue, L("EntityDynamicPropertyValue"));
            //dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Create, L("CreatingDynamicEntityPropertyValue"));
            //dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Edit, L("EditingDynamicEntityPropertyValue"));
            //dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Delete, L("DeletingDynamicEntityPropertyValue"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);
            pages.CreateChildPermission(AppPermissions.Pages_Analysis_Dashboard, L("AnalysisDashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UploadProcureData, L("ImportGlobusFromExcel"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, RMACTConsts.LocalizationSourceName);
        }
    }
}