using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult Index()
        {

            var roleList = db.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = db.Users.Select(users => new SelectListItem { Value = users.UserName.ToString(), Text = users.UserName }).ToList();
            ViewBag.Users = userList;

            return View();
        }

        //GET:CREATE ROLE
        public ActionResult CreatingRole()
        {
            return View();
        }

        //POST: CREATE ROLE
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
        //GET
        public ActionResult AddRoleToUser()
        {
            return View();
        }
        //POST
        [HttpPost]
        public ActionResult AddRoleToUser(string email, string role)
        {
            ApplicationUser user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
             if (UserHelper.AddRoleToUser(user.Id, role))
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
        //GET
        public ActionResult RemoveRoleFromUser()
        {
            return View();
        }
        //POST
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
                                      DailySalary=user.DailySalary,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Salary=p.DailySalary,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }
        //GET
        public ActionResult AddUser()
        {
            var roleList = db.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            return View();
        }
        //POST
        [HttpPost]
        public ActionResult AddUser(string email, string role,double? dailySalary)
        {
            UserHelper.CreateUser(email,dailySalary);
            ApplicationUser user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            UserHelper.AddRoleToUser(user.Id, role);
            ViewBag.message = "User Created Succesfully";
            var roleList = db.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = db.Users.Select(users => new SelectListItem { Value = users.UserName.ToString(), Text = users.UserName }).ToList();
            ViewBag.Users = userList;
            return View();
        }
        public ActionResult DeleteUser(string id)
        {
            var user = db.Users.Find(id);
            var prouser = db.ProjectUsers.Where(pj => pj.UserId == id).ToList();
            foreach (var p in prouser)
            {
                db.ProjectUsers.Remove(p);
                db.SaveChanges();
            }
            var tasks=db.Tasks.Where(t => t.UserId == id).ToList();
            foreach (var task in tasks)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        //Get
        public ActionResult EditUser(string id)
        {
            var user = db.Users.Find(id);

            return View(user);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(string Email,double DailySalary,string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(UserId);
                user.Email = Email;
                user.DailySalary = DailySalary;
                db.SaveChanges();
            }
            return RedirectToAction("Users");
        }

    }
}