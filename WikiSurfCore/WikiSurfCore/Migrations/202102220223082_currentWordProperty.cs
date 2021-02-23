namespace WikiSurfCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currentWordProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WikiSessions", "CurrentWordBankId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WikiSessions", "CurrentWordBankId");
        }
    }
}
