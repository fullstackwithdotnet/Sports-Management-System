namespace Sports_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event_Photos",
                c => new
                    {
                        Photo_ID = c.Int(nullable: false, identity: true),
                        Event_Photo = c.String(),
                        Event_PhotoTags = c.String(),
                        Event_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Photo_ID)
                .ForeignKey("dbo.Events", t => t.Event_ID, cascadeDelete: true)
                .Index(t => t.Event_ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Event_ID = c.Int(nullable: false, identity: true),
                        Game_ID = c.Int(nullable: false),
                        FeatureEvent = c.Boolean(nullable: false),
                        EventVenue = c.String(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        EventStartTime = c.DateTime(nullable: false),
                        EventEndTime = c.DateTime(nullable: false),
                        EventDescription = c.String(),
                    })
                .PrimaryKey(t => t.Event_ID)
                .ForeignKey("dbo.Games", t => t.Game_ID, cascadeDelete: true)
                .Index(t => t.Game_ID);
            
            CreateTable(
                "dbo.Event_Record",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Event_ID = c.Int(nullable: false),
                        Competitor_ID = c.Int(nullable: false),
                        Competitor_Position = c.Int(nullable: false),
                        Competitor_Medal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Competitors", t => t.Competitor_ID, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_ID, cascadeDelete: true)
                .Index(t => t.Event_ID)
                .Index(t => t.Competitor_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Event_Record", "Event_ID", "dbo.Events");
            DropForeignKey("dbo.Event_Record", "Competitor_ID", "dbo.Competitors");
            DropForeignKey("dbo.Event_Photos", "Event_ID", "dbo.Events");
            DropForeignKey("dbo.Events", "Game_ID", "dbo.Games");
            DropIndex("dbo.Event_Record", new[] { "Competitor_ID" });
            DropIndex("dbo.Event_Record", new[] { "Event_ID" });
            DropIndex("dbo.Events", new[] { "Game_ID" });
            DropIndex("dbo.Event_Photos", new[] { "Event_ID" });
            DropTable("dbo.Event_Record");
            DropTable("dbo.Events");
            DropTable("dbo.Event_Photos");
        }
    }
}
