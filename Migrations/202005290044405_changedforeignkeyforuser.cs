namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedforeignkeyforuser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProjectUsers", new[] { "User_Id1" });
            DropColumn("dbo.ProjectUsers", "User_Id");
            RenameColumn(table: "dbo.ProjectUsers", name: "User_Id1", newName: "User_Id");
            AlterColumn("dbo.ProjectUsers", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProjectUsers", "User_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjectUsers", new[] { "User_Id" });
            AlterColumn("dbo.ProjectUsers", "User_Id", c => c.String());
            RenameColumn(table: "dbo.ProjectUsers", name: "User_Id", newName: "User_Id1");
            AddColumn("dbo.ProjectUsers", "User_Id", c => c.String());
            CreateIndex("dbo.ProjectUsers", "User_Id1");
        }
    }
}
