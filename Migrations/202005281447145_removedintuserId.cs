namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedintuserId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProjectUsers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectUsers", "UserId", c => c.Int(nullable: false));
        }
    }
}
