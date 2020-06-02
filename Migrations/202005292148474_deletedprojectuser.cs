namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedprojectuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectUsers", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectUsers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectUsers", new[] { "ProjectId" });
            DropIndex("dbo.ProjectUsers", new[] { "UserId" });
            DropTable("dbo.ProjectUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ProjectUsers", "UserId");
            CreateIndex("dbo.ProjectUsers", "ProjectId");
            AddForeignKey("dbo.ProjectUsers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProjectUsers", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
