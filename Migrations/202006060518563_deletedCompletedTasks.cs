namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedCompletedTasks : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CompletedTaskModels");
        }
        
        public override void Down()
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
    }
}
