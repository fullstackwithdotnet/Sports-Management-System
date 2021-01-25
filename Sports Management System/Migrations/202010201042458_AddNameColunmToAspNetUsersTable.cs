namespace Sports_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameColunmToAspNetUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Names", c => c.String());

        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Names");
        }
    }
}
