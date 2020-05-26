namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedUserClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropIndex("dbo.ProjectUsers", new[] { "UserId" });
            DropIndex("dbo.Tasks", new[] { "UserId" });
            AddColumn("dbo.ProjectUsers", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Tasks", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProjectUsers", "User_Id");
            CreateIndex("dbo.Tasks", "User_Id");
            AddForeignKey("dbo.ProjectUsers", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Tasks", "User_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Tasks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectUsers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tasks", new[] { "User_Id" });
            DropIndex("dbo.ProjectUsers", new[] { "User_Id" });
            DropColumn("dbo.Tasks", "User_Id");
            DropColumn("dbo.ProjectUsers", "User_Id");
            CreateIndex("dbo.Tasks", "UserId");
            CreateIndex("dbo.ProjectUsers", "UserId");
            AddForeignKey("dbo.Tasks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
