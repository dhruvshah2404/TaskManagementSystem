using Microsoft.AspNet.Identity;
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
    [Authorize(Roles = "Project Manager")]
    public class ProjectsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var ProjectList = db.Projects.OrderBy(p=>p.Priority).ToList();
            return View(ProjectList);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name, string customerName, DateTime? deadline,Priority priority,double budget)
        {
            
            ProjectHelper.AddProject(name, customerName, deadline,priority,budget);
            ViewBag.message = "Project created succesfully";
            return View();
        }
        
        public ActionResult Delete(int ProjectId)
        {
            ProjectHelper.Delete(ProjectId);
            ViewBag.message = "Project Deleted";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Name,Customer,Deadline,Priority,Budget")]Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public ActionResult Info(int? id,string OrderBy)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Include(t=>t.Tasks).Include(u => u.ProjectUsers)
                .Where(x => x.Id == id)
               .FirstOrDefault();
            if (OrderBy=="priority")
            {
                ViewBag.taskList = project.Tasks.OrderBy(t => t.Priority);

            }
            else if(OrderBy=="percent")
            {
                ViewBag.taskList = project.Tasks.OrderByDescending(t => t.percentageCompleted);
            }
            else 
            {
                ViewBag.taskList = project.Tasks.OrderByDescending(t => t.percentageCompleted);
            }
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        public ActionResult AddUser(int projectId)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            
            var users = roleManager.FindByName("Developer").Users.Select(u => u.UserId).ToList();
            var developers = db.Users.Where(e => users.Contains(e.Id)).ToList();

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(developers, "Id", "UserName");
            ViewBag.Project = projectId;
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(int ProjectId, string UserId)
        {
            var ProjectUser = new ProjectUser() { ProjectId = ProjectId, UserId = UserId };
            if (ModelState.IsValid)
            {
                if (!db.ProjectUsers.Any(p=>p.ProjectId==ProjectId && p.UserId==UserId))
                {
                    try
                    {
                        db.ProjectUsers.Add(ProjectUser);
                        db.SaveChanges();
                        return RedirectToAction("Info", new { id = ProjectId });
                    }
                    catch (Exception )
                    {

                    }
                }
                else
                {
                    ViewBag.message = "User Already there";
                    return RedirectToAction("Info", new { id = ProjectId });
                }
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var users = roleManager.FindByName("Developer").Users.Select(u => u.UserId).ToList();
            var developers = db.Users.Where(e => users.Contains(e.Id)).ToList();

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(developers, "Id", "UserName");
            return View(ProjectUser);
        }

        public ActionResult RemoveFromProject(int projectId,string userId)
        {
            var project = db.ProjectUsers.FirstOrDefault(p => p.ProjectId == projectId && p.UserId == userId);
            db.ProjectUsers.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Info", new { id = projectId });
        }

        public ActionResult UserInfo(string UserId)
        {
            var user = db.Users.Find(UserId);
            return View(user);
        }


    }

}
            
