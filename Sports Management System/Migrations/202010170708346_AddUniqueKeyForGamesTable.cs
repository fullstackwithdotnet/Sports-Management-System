namespace Sports_Management_System.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUniqueKeyForGamesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "Game_Code", c => c.String(nullable: false, maxLength: 250));
            Sql("ALTER TABLE Games ADD CONSTRAINT UC_GameCode UNIQUE(Game_Code)");
        }

        public override void Down()
        {
            AlterColumn("dbo.Games", "Game_Code", c => c.String(nullable: false));
            Sql("ALTER TABLE Games ADD CONSTRAINT UC_GameCode");

        }
    }
}
