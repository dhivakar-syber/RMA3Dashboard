using System.Collections.Generic;
using SyberGate.RMACT.Masters.Dtos;
using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Models.Dtos;
using SyberGate.RMACT.Models;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using SyberGate.RMACT.Auditing.Dto;
using SyberGate.RMACT.Authorization.Accounts.Dto;
using SyberGate.RMACT.Authorization.Delegation;
using SyberGate.RMACT.Authorization.Permissions.Dto;
using SyberGate.RMACT.Authorization.Roles;
using SyberGate.RMACT.Authorization.Roles.Dto;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Authorization.Users.Delegation.Dto;
using SyberGate.RMACT.Authorization.Users.Dto;
using SyberGate.RMACT.Authorization.Users.Importing.Dto;
using SyberGate.RMACT.Authorization.Users.Profile.Dto;
using SyberGate.RMACT.Chat;
using SyberGate.RMACT.Chat.Dto;
using SyberGate.RMACT.DynamicEntityProperties.Dto;
using SyberGate.RMACT.Editions;
using SyberGate.RMACT.Editions.Dto;
using SyberGate.RMACT.Friendships;
using SyberGate.RMACT.Friendships.Cache;
using SyberGate.RMACT.Friendships.Dto;
using SyberGate.RMACT.Localization.Dto;
using SyberGate.RMACT.MultiTenancy;
using SyberGate.RMACT.MultiTenancy.Dto;
using SyberGate.RMACT.MultiTenancy.HostDashboard.Dto;
using SyberGate.RMACT.MultiTenancy.Payments;
using SyberGate.RMACT.MultiTenancy.Payments.Dto;
using SyberGate.RMACT.Notifications.Dto;
using SyberGate.RMACT.Organizations.Dto;
using SyberGate.RMACT.Sessions.Dto;
using SyberGate.RMACT.WebHooks.Dto;
using SyberGate.RMACT.Masters.Dto;
using SyberGate.RMACT.Tenants.Dashboard.Dto;
using SyberGate.RMACT.Masters.Importing.Dto;

namespace SyberGate.RMACT
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ApprovalUser, ImportApprovalUsersDto>().ReverseMap();
            configuration.CreateMap<SupportAttachmentsDto, SupportAttachments>().ReverseMap();
            configuration.CreateMap<CreateOrEditApprovalUserDto, ApprovalUser>().ReverseMap();
            configuration.CreateMap<ApprovalUserDto, ApprovalUser>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartBucketDto, PartBucket>().ReverseMap();
            configuration.CreateMap<PartBucketDto, PartBucket>().ReverseMap();
            configuration.CreateMap<ImportPartBucketDto,PartBucket>().ReverseMap();
            configuration.CreateMap<ImportPartBucketDto, RMTapTool>().ReverseMap();
            configuration.CreateMap<PartBucketDto, PartBucket>().ReverseMap();
            configuration.CreateMap<RMTapToolDto, RMTapTool>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartModelMatrixDto, PartModelMatrix>().ReverseMap();
            configuration.CreateMap<PartModelMatrixDto, PartModelMatrix>().ReverseMap();
            configuration.CreateMap<PartModelMatrix, ImportPartModelMatrixDto>().ReverseMap();
            //configuration.CreateMap<PartModelMatrix, ImportPartModelMatrixDto2>().ReverseMap();
            configuration.CreateMap<CreateOrEditLeadModelDto, LeadModel>().ReverseMap();
            configuration.CreateMap<LeadModelDto, LeadModel>().ReverseMap();
            configuration.CreateMap<LeadModel, ImportLeadModelsDto>().ReverseMap();
            configuration.CreateMap<CreateOrEditPlantDto, Plant>().ReverseMap();
            configuration.CreateMap<PlantDto, Plant>().ReverseMap();
            configuration.CreateMap<CreateOrEditProcureDataDto, GlobusData>().ReverseMap();
            configuration.CreateMap<ProcureDataDto, GlobusData>().ReverseMap();
            configuration.CreateMap<CreateOrEditRawMaterialMixtureDto, RawMaterialMixture>().ReverseMap();
            configuration.CreateMap<RawMaterialMixtureDto, RawMaterialMixture>().ReverseMap();
            configuration.CreateMap<CreateOrEditRawMaterialIndexDto, RawMaterialIndex>().ReverseMap();
            configuration.CreateMap<RawMaterialIndexDto, RawMaterialIndex>().ReverseMap();
            configuration.CreateMap<CreateOrEditIndexNameDto, IndexName>().ReverseMap();
            configuration.CreateMap<IndexNameDto, IndexName>().ReverseMap();
            configuration.CreateMap<CreateOrEditCommodityTreeDto, CommodityTree>().ReverseMap();
            configuration.CreateMap<CommodityTreeDto, CommodityTree>().ReverseMap();
            configuration.CreateMap<CreateOrEditA3DocumentDto, A3Document>().ReverseMap();
            configuration.CreateMap<A3DocumentDto, A3Document>().ReverseMap();
            configuration.CreateMap<CreateOrEditRMGroupDto, RMGroup>().ReverseMap();
            configuration.CreateMap<RMGroupDto, RMGroup>().ReverseMap();
            configuration.CreateMap<GetPriceImpactsp, GetRMPriceImpact>().ReverseMap();
            configuration.CreateMap<A3SubPartImpact, GetRMPriceImpact>().ReverseMap();
            configuration.CreateMap<A3PriceImpact, GetRMPriceImpact>().ReverseMap();
            configuration.CreateMap<List<GetPriceTrendsp>, List<GetRMPriceTrend>>().ReverseMap();
            configuration.CreateMap<CreateOrEditYearDto, Year>().ReverseMap();
            configuration.CreateMap<YearDto, Year>().ReverseMap();
            configuration.CreateMap<CreateOrEditUnitOfMeasurementDto, UnitOfMeasurement>().ReverseMap();
            configuration.CreateMap<UnitOfMeasurementDto, UnitOfMeasurement>().ReverseMap();
            configuration.CreateMap<CreateOrEditRawMaterialGradeDto, RawMaterialGrade>().ReverseMap();
            configuration.CreateMap<RawMaterialGradeDto, RawMaterialGrade>().ReverseMap();
            configuration.CreateMap<CreateOrEditPartDto, Part>().ReverseMap();
            configuration.CreateMap<PartDto, SubPartDto>().ReverseMap();
            configuration.CreateMap<PartDto, Part>().ReverseMap();
            configuration.CreateMap<SubPartDto, SubPart>().ReverseMap();
            configuration.CreateMap<CreateOrEditBaseRMRateDto, BaseRMRate>().ReverseMap();
            configuration.CreateMap<BaseRMRateDto, BaseRMRate>().ReverseMap();
            configuration.CreateMap<CreateOrEditUOMDto, UOM>().ReverseMap();
            configuration.CreateMap<UOMDto, UOM>().ReverseMap();
            configuration.CreateMap<CreateOrEditBuyerDto, Buyer>().ReverseMap();
            configuration.CreateMap<BuyerDto, Buyer>().ReverseMap();
            configuration.CreateMap<CreateOrEditSupplierDto, Supplier>().ReverseMap();
            configuration.CreateMap<SupplierDto, Supplier>().ReverseMap();
            configuration.CreateMap<CreateOrEditMedicalSpecialtyDto, MedicalSpecialty>().ReverseMap();
            configuration.CreateMap<MedicalSpecialtyDto, MedicalSpecialty>().ReverseMap();
            configuration.CreateMap<CreateOrEditIcd10Dto, Icd10>().ReverseMap();
            configuration.CreateMap<Icd10Dto, Icd10>().ReverseMap();
            configuration.CreateMap<CreateOrEditConstantDto, Constant>().ReverseMap();
            configuration.CreateMap<ConstantDto, Constant>().ReverseMap();
            configuration.CreateMap<CreateOrEditGenderDto, Gender>().ReverseMap();
            configuration.CreateMap<GenderDto, Gender>().ReverseMap();
            configuration.CreateMap<CreateOrEditMedicinalTypeDto, MedicinalType>().ReverseMap();
            configuration.CreateMap<MedicinalTypeDto, MedicinalType>().ReverseMap();
            configuration.CreateMap<CreateOrEditEthnicityDto, Ethnicity>().ReverseMap();
            configuration.CreateMap<EthnicityDto, Ethnicity>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.PropertyName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}