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
            //modelBuilder
            //    .Entity<WordBank>()
            //    .HasRequired(x => x.WikiPage)
            //    .WithRequiredDependent(x => x.WordBank);

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
    }
}
