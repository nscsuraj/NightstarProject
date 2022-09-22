using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace PartnerPortal.Repository
{
    public class ChatDataContext : DbContext
    {
         public ChatDataContext()
            : base("name=ChatDatabase")
        {
        }
        
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