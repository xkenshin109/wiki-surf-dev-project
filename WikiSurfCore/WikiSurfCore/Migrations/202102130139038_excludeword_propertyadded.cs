namespace WikiSurfCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class excludeword_propertyadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WordBanks", "Exclude", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WordBanks", "Exclude");
        }
    }
}
