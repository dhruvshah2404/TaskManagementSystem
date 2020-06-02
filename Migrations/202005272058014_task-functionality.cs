namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskfunctionality : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tasks", new[] { "User_Id" });
            DropColumn("dbo.Tasks", "UserId");
            RenameColumn(table: "dbo.Tasks", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Tasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tasks", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tasks", new[] { "UserId" });
            AlterColumn("dbo.Tasks", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Tasks", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Tasks", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "User_Id");
        }
    }
}
