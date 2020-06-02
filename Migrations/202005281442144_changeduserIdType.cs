namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeduserIdType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectUsers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectUsers", new[] { "User_Id" });
            AddColumn("dbo.ProjectUsers", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.ProjectUsers", "User_Id", c => c.String());
            CreateIndex("dbo.ProjectUsers", "User_Id1");
            AddForeignKey("dbo.ProjectUsers", "User_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUsers", "User_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectUsers", new[] { "User_Id1" });
            AlterColumn("dbo.ProjectUsers", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.ProjectUsers", "User_Id1");
            CreateIndex("dbo.ProjectUsers", "User_Id");
            AddForeignKey("dbo.ProjectUsers", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
