namespace TaskManagementSystem.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskManagementSystem.Models;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //Create User
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser { Email = "projectmanager@email.com", UserName = "projectmanager@email.com" };
            manager.Create(user, "Spiderweb1!");
            context.SaveChanges();

            //Create Role for User
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var ProjectManagerRole = new IdentityRole("Project Manager");
            roleManager.Create(ProjectManagerRole);
            context.SaveChanges();

            //Assign Role to User
            manager.AddToRole(user.Id, ProjectManagerRole.Name);
            context.SaveChanges();
        }
    }
}
