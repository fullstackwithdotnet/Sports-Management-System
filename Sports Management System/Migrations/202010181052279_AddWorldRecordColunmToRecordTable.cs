namespace Sports_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorldRecordColunmToRecordTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event_Record", "WorldRecord", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event_Record", "WorldRecord");
        }
    }
}
