namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPriorityEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Priority", c => c.Int(nullable: true));
            AddColumn("dbo.Tasks", "Priority", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Priority");
            DropColumn("dbo.Projects", "Priority");
        }
    }
}
