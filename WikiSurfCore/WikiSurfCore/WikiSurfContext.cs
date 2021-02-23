using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiSurfModel;

namespace WikiSurfCore
{
    public partial class WikiSurfContext : DbContext
    {
        public WikiSurfContext():
            base("name=WikiSurfContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WikiSessionWordBank>()
                .HasRequired<WikiSession>(s => s.WikiSession)
                .WithMany(g => g.WikiSessionWordBanks)
                .HasForeignKey<Guid>(s => s.WikiSessionId);

            modelBuilder.Entity<WordBankQueue>()
                .HasRequired<WordBank>(x => x.WordBank)
                .WithMany(g => g.WordBankQueues)
                .HasForeignKey<Guid>(s => s.WordBankId);

            modelBuilder.Entity<WordBankXref>()
                .HasRequired<WordBank>(s => s.WordBank)
                .WithMany(g => g.WordBankXrefs)
                .HasForeignKey<Guid>(s => s.WordBankId);

            var convention = new AttributeToColumnAnnotationConvention<DefaultValueAttribute, string>("SqlDefaultValue", (p, attributes) => attributes.SingleOrDefault().Value.ToString());
            modelBuilder.Conventions.Add(convention);
        }
        public virtual DbSet<WordBank> WordBanks { get; set; }
        public virtual DbSet<WordBankQueue> WordBankQueues { get; set; }
        public virtual DbSet<WikiPage> WikiPages { get; set; }
        public virtual DbSet<WikiLanguages> WikiLanguages { get; set; }
        public virtual DbSet<WikiLinks> WikiLinks { get; set; }
        public virtual DbSet<WikiProperties> WikiProperties { get; set; }
        public virtual DbSet<WikiSections> WikiSections { get; set; }
        public virtual DbSet<WikiCategories> WikiCategories { get; set; }
        public virtual DbSet<WikiSession> WikiSessions { get; set; }
        public virtual DbSet<WikiSessionWordBank> WikiSessionWordBanks { get; set; }
    }
}
