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
            var context = new ApplicationDbContext();//creating new dbContext

            var roleList = context.Roles.Select(roles => new SelectListItem {Value=roles.Name.ToString(),Text=roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = context.Users.Select(users => new SelectListItem {Value=users.UserName.ToString(),Text= users.UserName }).ToList();
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
            UserHelper.AddUserToRole()
            ViewBag.message = "Role Added to User";

            var roleList = context.Roles.Select(roles => new SelectListItem { Value = roles.Name.ToString(), Text = roles.Name }).ToList();
            ViewBag.Roles = roleList;
            var userList = context.Users.Select(users => new SelectListItem { Value = users.UserName.ToString(), Text = users.UserName }).ToList();
            ViewBag.Users = userList;
            return View("Index");
        }
    }
}