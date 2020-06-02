namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedcustomername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Customer", c => c.String());
            DropColumn("dbo.Projects", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "CustomerName", c => c.String());
            DropColumn("dbo.Projects", "Customer");
        }
    }
}
