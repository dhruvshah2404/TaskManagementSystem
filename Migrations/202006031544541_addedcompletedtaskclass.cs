namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcompletedtaskclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompletedTaskModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        TaskName = c.String(),
                        TaskDesc = c.String(),
                        SubmissionDate = c.DateTime(),
                        DeveloperName = c.String(),
                        ProjectName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompletedTaskModels");
        }
    }
}
