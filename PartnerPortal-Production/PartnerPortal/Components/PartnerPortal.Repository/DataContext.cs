using PartnerPortal.Domain.Admin;
using PartnerPortal.Domain.Pages;
using PartnerPortal.Domain.SiteUtility;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PartnerPortal.Domain.Accounts;
using PartnerPortal.Domain.CMS;
using PartnerPortal.Domain.Gateway;
using PartnerPortal.Domain.Import;


namespace PartnerPortal.Repository
{
    public class DataContext : DbContext
    {
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserSession> UserSession { get; set; }
        public DbSet<MegaMenu> Menu { get; set; }


        public DbSet<PageInfo> PageInfo { get; set; }

        public DbSet<CMS_ElementProperty> CmsElementProperty { get; set; }

       public DbSet<UploadInformation> UploadInformation { get; set; }

        public DbSet<SystemConfig> SystemConfig { get; set; }
        public DbSet<MetaTags> MetaTags { get; set; }
        public DbSet<SFAccounts> SFAccounts { get; set; }
        public DbSet<SFUserSession> SFUserSession { get; set; }
        public DbSet<SFTempSessionData> SFTempSessionData { get; set; }
        public DbSet<SalesforceAuthentication> SalesforceAuthentication { get; set; }
        public DbSet<SFTempSessionMdfData> SFTempSessionMdfData { get; set; }

        public DbSet<SFTempSessionOpportunityData> SFTempSessionOpportunityData { get; set; }
        public DbSet<OpportunityAdded> OpportunityAdded { get; set; }
        public DbSet<SFTempSessionOpportunityProducts> SFTempSessionOpportunityProducts { get; set; }
        public DbSet<SFTempSessionPurchaseByDistributors> SFTempSessionPurchaseByDistributors { get; set; }
        public DbSet<SFTempSessionPurchaseByProductClasses> SFTempSessionPurchaseByProductClasses { get; set; }
        public DbSet<PortalItemMaster> PortalItemMaster { get; set; }
        public DbSet<ProductClassToDisplay> ProductClassToDisplay { get; set; }
        public DbSet<FileTypeConfig> FileTypeConfig { get; set; }
        public DbSet<LibraryCategory> LibraryCategory { get; set; }
        public DbSet<LibraryFiles> LibraryFiles { get; set; }
        public DbSet<PageViews> PageViews { get; set; }
        public DbSet<MdfAdded> MdfAdded { get; set; }
        public DbSet<AdminUsers> AdminUsers { get; set; }
        public DbSet<PasswordGeneraionLog> PasswordGeneraionLog { get; set; }

        public DbSet<SFTempSessionDelegateData> DelegateReport { get; set; }
        public DbSet<SFTempSessionDelegateRebateItemData> DelegateRebateItemReport { get; set; }
        public DbSet<RebateRequest> RebateRequest { get; set; }
        public DbSet<RebateRequestDetail> RebateRequestDetail { get; set; }

        public DbSet<RebateRequestFiles> RebateRequestFiles { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<NotificationRecipients> NotificationRecipients { get; set; }
        public DbSet<NotificationReadBy> NotificationReadBy { get; set; }
        public DbSet<SFTempSessionLoyaltRegistration> SFTempSessionLoyaltRegistration { get; set; }

        public DbSet<SFTempSessionDemoUnitRequested> SFTempSessionDemoUnitRequested { get; set; }
        public DbSet<SFTempSessionTrainingRequested> SFTempSessionTrainingRequested { get; set; }
        public DbSet<ImportedMarketingMaterial> ImportedMarketingMaterial { get; set; }
        public DbSet<ImportedMarketingMaterialCategories> ImportedMarketingMaterialCategories { get; set; }
        public DbSet<ImportedMarketingMaterialImages> ImportedMarketingMaterialImages { get; set; }
        public DbSet<ImportedResources> ImportedResource { get; set; }
        public virtual void Commit()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}