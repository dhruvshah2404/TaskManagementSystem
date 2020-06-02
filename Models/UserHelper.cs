using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{ //This class is used to add , delete and update users and roles , assign roles to the users
    public static class UserHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>
            (new UserStore<ApplicationUser>(db));
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>
            (new RoleStore<IdentityRole>(db));

        //Add User
        public static void CreateUser(string email, string password = "Examplepass1!")
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = email;
            user.UserName = email;

            userManager.Create(user, password);
            db.SaveChanges();

        }
        //Delete User
        public static void RemoveUser(string email)
        {
            var user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            userManager.Delete(user);
            db.SaveChanges();
        }
        //Create Role
        public static bool CreateRole(string roleName)
        {
            if (roleManager.RoleExists(roleName))
            {
                return false;
            }
            else
            {
                IdentityRole newRole = new IdentityRole(roleName);
                roleManager.Create(newRole);
                db.SaveChanges();

                return true;
            }
        }
        //Delete Role
        public static void DeleteRole(string roleName)
        {
            if (roleManager.RoleExists(roleName))
            {
                roleManager.Delete(new IdentityRole { Name = roleName });
                db.SaveChanges();
            }
        }
        public static bool CheckIfUserIsInRole(string userId, string role)
        {
            var result = userManager.IsInRole(userId, role);
            return result;
        }
        //Add Role To User
        public static bool AddRoleToUser(string userId, string role)
        {
            if (CheckIfUserIsInRole(userId, role))
            {
                return false;
            }
            else
            {
               
                userManager.AddToRole(userId, role);
                db.SaveChanges();
                return true;
            }
        }
        //Remove Role from User
        public static bool RemoveUserFromRole(string userId, string role)
        {
            if (!CheckIfUserIsInRole(userId, role))
            {
                return false;
            }
            else
            {
                userManager.RemoveFromRole(userId, role);
                db.SaveChanges();
                return true;
            }
        }
        //Get All Roles in System
        public static List<string> GetAllRoles()
        {
            return db.Roles.Select(r => r.Name).ToList();
        }

    }
}