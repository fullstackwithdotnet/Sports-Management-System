namespace Sports_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles (Id,Name) VALUES (1,'Admin')");
            Sql("INSERT INTO AspNetRoles (Id,Name) VALUES (2,'ScoreManager')");
        }

        public override void Down()
        {
            Sql("DELETE FROM AspNetRoles WHERE Id (SELECT Id FROM AspNetRoles WHERE Name = 'Admin')");
            Sql("DELETE FROM AspNetRoles WHERE Id (SELECT Id FROM AspNetRoles WHERE Name = 'ScoreManager')");
        }
    }
}
