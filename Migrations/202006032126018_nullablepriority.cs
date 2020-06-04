namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablepriority : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Priority", c => c.Int());
            AlterColumn("dbo.Tasks", "Priority", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Priority", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "Priority", c => c.Int(nullable: false));
        }
    }
}
