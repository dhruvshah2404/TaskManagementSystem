namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSalaryToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DailySalary", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DailySalary");
        }
    }
}
