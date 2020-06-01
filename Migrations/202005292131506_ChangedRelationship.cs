namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRelationship : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProjectUsers", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.ProjectUsers", name: "IX_User_Id", newName: "IX_UserId");
            CreateTable(
                "dbo.ApplicationUserProjects",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Project_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ApplicationUserProjects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserProjects", new[] { "Project_Id" });
            DropIndex("dbo.ApplicationUserProjects", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserProjects");
            RenameIndex(table: "dbo.ProjectUsers", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.ProjectUsers", name: "UserId", newName: "User_Id");
        }
    }
}
