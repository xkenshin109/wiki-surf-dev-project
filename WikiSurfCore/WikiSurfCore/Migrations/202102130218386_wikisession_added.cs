namespace WikiSurfCore.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class wikisession_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WikiSessions",
                c => new
                    {
                        WikiSessionId = c.Guid(nullable: false, identity: true),
                        PlayerName = c.String(),
                        TotalClicks = c.Int(nullable: false),
                        TotalWarnings = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        UpdateDateTime = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        CreatedDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.WikiSessionId);
            
            CreateTable(
                "dbo.WikiSessionWordBanks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WikiSessionId = c.Guid(nullable: false),
                        WordBankId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WikiSessions", t => t.WikiSessionId, cascadeDelete: true)
                .ForeignKey("dbo.WordBanks", t => t.WordBankId, cascadeDelete: true)
                .Index(t => t.WikiSessionId)
                .Index(t => t.WordBankId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WikiSessionWordBanks", "WordBankId", "dbo.WordBanks");
            DropForeignKey("dbo.WikiSessionWordBanks", "WikiSessionId", "dbo.WikiSessions");
            DropIndex("dbo.WikiSessionWordBanks", new[] { "WordBankId" });
            DropIndex("dbo.WikiSessionWordBanks", new[] { "WikiSessionId" });
            DropTable("dbo.WikiSessionWordBanks");
            DropTable("dbo.WikiSessions",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "IsActive",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                    {
                        "UpdateDateTime",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
        }
    }
}
