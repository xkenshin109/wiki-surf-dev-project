namespace WikiSurfCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wordAssociationXrefTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WordBankXrefs",
                c => new
                    {
                        WordBankXrefId = c.Guid(nullable: false, identity: true),
                        WordBankId = c.Guid(nullable: false),
                        Word = c.String(),
                    })
                .PrimaryKey(t => t.WordBankXrefId)
                .ForeignKey("dbo.WordBanks", t => t.WordBankId, cascadeDelete: true)
                .Index(t => t.WordBankId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WordBankXrefs", "WordBankId", "dbo.WordBanks");
            DropIndex("dbo.WordBankXrefs", new[] { "WordBankId" });
            DropTable("dbo.WordBankXrefs");
        }
    }
}
