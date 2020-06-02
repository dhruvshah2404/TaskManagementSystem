using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{ //This class is used to add , delete and update users and roles , assign roles to the users
    public static class UserManager
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>
            (new UserStore<ApplicationUser>(db));
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>
            (new RoleStore<IdentityRole>(db));

        public static void CreateUser()
        {

        }
        public static void DeleteUser()
        {

        }
        public static void CreateRole()
        {

        }
        public static void DeleteRole()
        {

        }
        public static bool CheckIfUserIsInRole(string userId, string role)
        {
            var result = userManager.IsInRole(userId, role);
            return result;
        }
        public static bool AddUserToRole(string userId, string role)
        {
            if (CheckIfUserIsInRole(userId, role))
            {
                return false;
            }
            else
            {
                userManager.AddToRole(userId, role);
                return true;
            }
        }
    }
}