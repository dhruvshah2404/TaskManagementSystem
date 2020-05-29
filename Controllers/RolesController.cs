using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Authorize(Roles = "Project Manager")]
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {

            var roleList = db.Roles.Select(roles => new SelectListItem {Value=roles.Name.ToString(),Text=roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = db.Users.Select(users => new SelectListItem {Value=users.UserName.ToString(),Text= users.UserName }).ToList();
            ViewBag.Users = userList;

            return View();
        }

        public ActionResult CreatingRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatingRole(string role)
        {
            try
            {
                UserHelper.CreateRole(role);
                ViewBag.message = "Role Created";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AddRoleToUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRoleToUser(string email,string role)
        {
            ApplicationUser user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if(UserHelper.AddRoleToUser(user.Id, role))
            {
                ViewBag.message = "Role Added to User";
            }
            else
            {
                ViewBag.message = "User is already in this Role";
            }

            var roleList = db.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = db.Users.Select(users => new SelectListItem { Value = users.UserName.ToString(), Text = users.UserName }).ToList();
            ViewBag.Users = userList;
            return View("Index");
        }
        public ActionResult RemoveRoleFromUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RemoveRoleFromUser(string email, string role) 
        {
            ApplicationUser user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (UserHelper.RemoveUserFromRole(user.Id, role))
            { 
                ViewBag.message = "Role Removed from User"; 
            }
            else
            {
                ViewBag.message = "User does not belong to this Role";
            }

            var roleList = db.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = db.Users.Select(users => new SelectListItem { Value = users.UserName.ToString(), Text = users.UserName }).ToList();
            ViewBag.Users = userList;
            return View("Index");
        }
        public ActionResult Users()
        {
            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name=="developer").ToList()
                                  }).ToList().Select(p => new UserViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }
        public ActionResult AddUser()
        {
            var roleList = db.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(string email,string role)
        {
            UserHelper.CreateUser(email);
            ApplicationUser user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            UserHelper.AddRoleToUser(user.Id, role);
            ViewBag.message = "User Created Succesfully";
            var roleList = db.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = db.Users.Select(users => new SelectListItem { Value = users.UserName.ToString(), Text = users.UserName }).ToList();
            ViewBag.Users = userList;
            return View();
        }
    }
}