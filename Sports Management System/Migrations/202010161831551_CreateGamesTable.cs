namespace Sports_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGamesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Game_ID = c.Int(nullable: false, identity: true),
                        Game_Code = c.String(nullable: false),
                        Game_Name = c.String(nullable: false),
                        Game_DurationInHours = c.Int(nullable: false),
                        Game_Description = c.String(),
                        Game_RulesBooklet = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Game_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Games");
        }
    }
}
