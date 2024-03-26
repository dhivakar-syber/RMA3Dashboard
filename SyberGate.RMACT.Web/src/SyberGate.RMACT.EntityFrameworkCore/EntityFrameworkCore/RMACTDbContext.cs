using SyberGate.RMACT.Masters;
using SyberGate.RMACT.Models;
using Abp.IdentityServer4;
using Abp.Organizations;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyberGate.RMACT.Authorization.Delegation;
using SyberGate.RMACT.Authorization.Roles;
using SyberGate.RMACT.Authorization.Users;
using SyberGate.RMACT.Chat;
using SyberGate.RMACT.Editions;
using SyberGate.RMACT.Friendships;
using SyberGate.RMACT.MultiTenancy;
using SyberGate.RMACT.MultiTenancy.Accounting;
using SyberGate.RMACT.MultiTenancy.Payments;
using SyberGate.RMACT.Storage;

namespace SyberGate.RMACT.EntityFrameworkCore
{
    public class RMACTDbContext : AbpZeroDbContext<Tenant, Role, User, RMACTDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<SupportAttachments> SupportAttachments { get; set; }
        public virtual DbSet<ApprovalUser> ApprovalUsers { get; set; }

        public virtual DbSet<A3RMTapTool> A3RMTapTool { get; set; }
        public virtual DbSet<RMTapTool> RMTapTool { get; set; }

        public virtual DbSet<A3PartBucket> A3PartBuckets { get; set; }

        public virtual DbSet<PartBucket> PartBuckets { get; set; }

        public virtual DbSet<A3LeadModel>A3LeadModels { get; set; }

        public virtual DbSet<A3PartModelMatrix> A3PartModelMatrixes { get; set; }

        public virtual DbSet<PartModelMatrix> PartModelMatrixes { get; set; }

        public virtual DbSet<LeadModel> LeadModels { get; set; }

        public virtual DbSet<Plant> Plants { get; set; }

        public virtual DbSet<GlobusData> ProcureDatas { get; set; }

        public virtual DbSet<RawMaterialMixture> RawMaterialMixtures { get; set; }

        public virtual DbSet<RawMaterialIndex> RawMaterialIndexes { get; set; }

        public virtual DbSet<IndexName> IndexNames { get; set; }

        public virtual DbSet<CommodityTree> CommodityTrees { get; set; }

        public virtual DbSet<A3SubPartImpact> A3SubPartImpacts { get; set; }

        public virtual DbSet<A3PriceTrend> A3PriceTrends { get; set; }

        public virtual DbSet<A3PriceImpact> A3PriceImpacts { get; set; }

        public virtual DbSet<A3Document> A3Documents { get; set; }

        public virtual DbSet<RMGroup> RMGroups { get; set; }

        public virtual DbSet<SupplierBuyerMap> SupplierBuyerMaps { get; set; }

        public virtual DbSet<Year> Years { get; set; }

        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }

        public virtual DbSet<RawMaterialGrade> RawMaterialGrades { get; set; }

        public virtual DbSet<GlobusData> GlobusDatas { get; set; }

        public virtual DbSet<Part> Parts { get; set; }

        public virtual DbSet<SubPart> SubParts { get; set; }

        public virtual DbSet<BaseRMRate> BaseRMRates { get; set; }

        public virtual DbSet<UOM> UOMs { get; set; }

        public virtual DbSet<Buyer> Buyers { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }

        public virtual DbSet<Icd10> Icd10s { get; set; }

        public virtual DbSet<Constant> Constants { get; set; }

        public virtual DbSet<Gender> Gender { get; set; }

        public virtual DbSet<MedicinalType> MedicinalTypes { get; set; }

        public virtual DbSet<Ethnicity> Ethnicities { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public RMACTDbContext(DbContextOptions<RMACTDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            
 modelBuilder.Entity<MedicalSpecialty>(m =>
            {
                m.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<Icd10>(i =>
                       {
                           i.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Constant>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Gender>(g =>
                       {
                           g.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<MedicinalType>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Ethnicity>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}