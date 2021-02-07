namespace WikiSurfCore.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WikiCategories",
                c => new
                    {
                        WikiCategoriesId = c.Guid(nullable: false, identity: true),
                        WikiPageId = c.Guid(nullable: false),
                        SortKey = c.String(),
                        Hidden = c.String(),
                        Additional = c.String(),
                        ImportDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.WikiCategoriesId)
                .ForeignKey("dbo.WikiPages", t => t.WikiPageId, cascadeDelete: true)
                .Index(t => t.WikiPageId);
            
            CreateTable(
                "dbo.WikiPages",
                c => new
                    {
                        WikiPageId = c.Guid(nullable: false, identity: true),
                        WordBankId = c.Guid(nullable: false),
                        PageId = c.Long(nullable: false),
                        RevId = c.Long(nullable: false),
                        Title = c.String(),
                        DisplayTitle = c.String(),
                        PlainTextContent = c.String(),
                        ImportDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.WikiPageId)
                .ForeignKey("dbo.WordBanks", t => t.WordBankId, cascadeDelete: true)
                .Index(t => t.WordBankId);
            
            CreateTable(
                "dbo.WikiLanguages",
                c => new
                    {
                        WikiLanguagesId = c.Guid(nullable: false, identity: true),
                        WikiPageId = c.Guid(nullable: false),
                        Language = c.String(),
                        Url = c.String(),
                        LanguageName = c.String(),
                        Autonym = c.String(),
                        Add = c.String(),
                        ImportDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.WikiLanguagesId)
                .ForeignKey("dbo.WikiPages", t => t.WikiPageId, cascadeDelete: true)
                .Index(t => t.WikiPageId);
            
            CreateTable(
                "dbo.WikiLinks",
                c => new
                    {
                        WikiLinksId = c.Guid(nullable: false, identity: true),
                        WikiPageId = c.Guid(nullable: false),
                        Ns = c.String(),
                        Exists = c.String(),
                        Additional = c.String(),
                        ImportDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.WikiLinksId)
                .ForeignKey("dbo.WikiPages", t => t.WikiPageId, cascadeDelete: true)
                .Index(t => t.WikiPageId);
            
            CreateTable(
                "dbo.WikiProperties",
                c => new
                    {
                        WikiPropertiesId = c.Guid(nullable: false),
                        WikiPageId = c.Guid(nullable: false),
                        Name = c.String(),
                        Additional = c.String(),
                        ImportDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.WikiPropertiesId)
                .ForeignKey("dbo.WikiPages", t => t.WikiPageId, cascadeDelete: true)
                .Index(t => t.WikiPageId);
            
            CreateTable(
                "dbo.WikiSections",
                c => new
                    {
                        WikiSectionsId = c.Guid(nullable: false, identity: true),
                        WikiPageId = c.Guid(nullable: false),
                        ToCLevel = c.Int(nullable: false),
                        Level = c.String(),
                        Line = c.String(),
                        Number = c.String(),
                        Index = c.String(),
                        FromTitle = c.String(),
                        ByteOffset = c.Int(nullable: false),
                        Anchor = c.String(),
                        ImportDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                    })
                .PrimaryKey(t => t.WikiSectionsId)
                .ForeignKey("dbo.WikiPages", t => t.WikiPageId, cascadeDelete: true)
                .Index(t => t.WikiPageId);
            
            CreateTable(
                "dbo.WordBanks",
                c => new
                    {
                        WordBankId = c.Guid(nullable: false, identity: true),
                        Word = c.String(),
                        WordIndex = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.WordBankId);
            
            CreateTable(
                "dbo.WordBankQueues",
                c => new
                    {
                        WordBankQueueId = c.Guid(nullable: false, identity: true),
                        WordBankId = c.Guid(nullable: false),
                        IsProcessed = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ProcessedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.WordBankQueueId)
                .ForeignKey("dbo.WordBanks", t => t.WordBankId, cascadeDelete: true)
                .Index(t => t.WordBankId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WordBankQueues", "WordBankId", "dbo.WordBanks");
            DropForeignKey("dbo.WikiCategories", "WikiPageId", "dbo.WikiPages");
            DropForeignKey("dbo.WikiPages", "WordBankId", "dbo.WordBanks");
            DropForeignKey("dbo.WikiSections", "WikiPageId", "dbo.WikiPages");
            DropForeignKey("dbo.WikiProperties", "WikiPageId", "dbo.WikiPages");
            DropForeignKey("dbo.WikiLinks", "WikiPageId", "dbo.WikiPages");
            DropForeignKey("dbo.WikiLanguages", "WikiPageId", "dbo.WikiPages");
            DropIndex("dbo.WordBankQueues", new[] { "WordBankId" });
            DropIndex("dbo.WikiSections", new[] { "WikiPageId" });
            DropIndex("dbo.WikiProperties", new[] { "WikiPageId" });
            DropIndex("dbo.WikiLinks", new[] { "WikiPageId" });
            DropIndex("dbo.WikiLanguages", new[] { "WikiPageId" });
            DropIndex("dbo.WikiPages", new[] { "WordBankId" });
            DropIndex("dbo.WikiCategories", new[] { "WikiPageId" });
            DropTable("dbo.WordBankQueues",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
            DropTable("dbo.WordBanks");
            DropTable("dbo.WikiSections",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "ImportDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
            DropTable("dbo.WikiProperties",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "ImportDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
            DropTable("dbo.WikiLinks",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "ImportDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
            DropTable("dbo.WikiLanguages",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "ImportDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
            DropTable("dbo.WikiPages",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "ImportDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
            DropTable("dbo.WikiCategories",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "ImportDate",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                });
        }
    }
}
