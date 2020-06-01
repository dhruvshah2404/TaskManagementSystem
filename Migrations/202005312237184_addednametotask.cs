namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednametotask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Name");
        }
    }
}
