namespace Sports_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCompetitorsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competitors",
                c => new
                    {
                        Competitor_ID = c.Int(nullable: false, identity: true),
                        Competitor_Salutation = c.String(maxLength: 100),
                        Competitor_Name = c.String(nullable: false, maxLength: 250),
                        Competitor_DoB = c.DateTime(nullable: false),
                        Competitor_Email = c.String(nullable: false, maxLength: 100),
                        Competitor_Description = c.String(maxLength: 100),
                        Competitor_Country = c.String(nullable: false),
                        Competitor_Gender = c.Int(nullable: false),
                        Competitor_ContactNo = c.String(),
                        Competitor_Website = c.String(),
                        Competitor_Photo = c.String(),
                    })
                .PrimaryKey(t => t.Competitor_ID)
                .Index(t => t.Competitor_Email, unique: true);
            
            CreateTable(
                "dbo.GameCompetitors",
                c => new
                    {
                        Game_Game_ID = c.Int(nullable: false),
                        Competitor_Competitor_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Game_ID, t.Competitor_Competitor_ID })
                .ForeignKey("dbo.Games", t => t.Game_Game_ID, cascadeDelete: true)
                .ForeignKey("dbo.Competitors", t => t.Competitor_Competitor_ID, cascadeDelete: true)
                .Index(t => t.Game_Game_ID)
                .Index(t => t.Competitor_Competitor_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameCompetitors", "Competitor_Competitor_ID", "dbo.Competitors");
            DropForeignKey("dbo.GameCompetitors", "Game_Game_ID", "dbo.Games");
            DropIndex("dbo.GameCompetitors", new[] { "Competitor_Competitor_ID" });
            DropIndex("dbo.GameCompetitors", new[] { "Game_Game_ID" });
            DropIndex("dbo.Competitors", new[] { "Competitor_Email" });
            DropTable("dbo.GameCompetitors");
            DropTable("dbo.Competitors");
        }
    }
}
