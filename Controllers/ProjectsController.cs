﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class ProjectsController : Controller
    {
        public ProjectsController()
        {

        }
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Projects
        public ActionResult Index()
        {
            var ProjectList = db.Projects.ToList();
            return View(ProjectList);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string name, string customerName, DateTime? deadline)
        {
            ProjectHelper.AddProject(name, customerName, deadline);
            ViewBag.message = "Project created succesfully";
            return View();
        }

        public ActionResult Delete(int ProjectId)
        {
            ProjectHelper.Delete(ProjectId);
            ViewBag.message = "Project Deleted";
            return RedirectToAction("Index");
        }
        public ActionResult Info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Include(p => p.Tasks).Include(u => u.ProjectUsers).Single(x => x.Id == id);

            if (id == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        public ActionResult AddUser(int projectId)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var users=roleManager.FindByName("Developer").Users.Select(u=>u.UserId).ToList();
            var developers = db.Users.Where(e => users.Contains(e.Id)).ToList();

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(developers, "Id", "UserName");
            ViewBag.Project = projectId;
            //var project = db.Projects.Find(projectId);
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(int ProjectId, string UserId)
        {
            var user = db.Users.Find(UserId);
            var project = db.Projects.Find(ProjectId);
            var ProjectUser = new ProjectUser() { ProjectId = ProjectId, User_Id = UserId};
            if (ModelState.IsValid)
            {
                if (!db.ProjectUsers.Any(p=>p.User_Id==UserId && p.ProjectId==ProjectId))
                {
                    db.ProjectUsers.Add(ProjectUser);
                    user.ProjectUsers.Add(ProjectUser);
                    project.ProjectUsers.Add(ProjectUser);
                    db.SaveChanges();
                    ViewBag.message = "User Added Succesfully";
                    return RedirectToAction("Info",new { id=ProjectId});
                }
            }

            return View(ProjectUser);
        }
    }
}
