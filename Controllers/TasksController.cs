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
    public class TasksController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            
            var tasks = db.Tasks.Include(t => t.Project).Include(t => t.User);
            return View(tasks.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        public ActionResult Create(int projectId,string userId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, string description, int projectId, string userId, DateTime SubmissionDate, Priority priority)
        {
            if (ModelState.IsValid)
            {
               TaskHelper.Add(name, description, projectId, userId, SubmissionDate, priority);
            }
            return RedirectToAction("Info","Projects",new { id=projectId });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tasks.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", tasks.UserId);
            return View(tasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,percentageCompleted,IsCompleted,SubmissionDate,ProjectId,UserId,Priority")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                if (tasks.percentageCompleted==100)
                {
                    tasks.IsCompleted = true;
                }
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tasks.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", tasks.UserId);
            return View(tasks);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskHelper.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [OverrideAuthorization]
        [Authorize(Roles = "Developer")]
        public ActionResult GetAllTasks(string id)
        {
            var tasks = db.Tasks.Where(t => t.UserId == id).ToList();
            return View(tasks);
        }

        [OverrideAuthorization]
        [Authorize(Roles ="Developer")]
        public ActionResult TaskDetails(int? id)
        {
            var task = db.Tasks.Find(id);
            return View(task);
        }

        [OverrideAuthorization]
        [Authorize(Roles = "Developer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaskDetails(int? percentageCompleted,string UrgentNotes,int taskId)
        {
            var task = db.Tasks.First(t => t.Id == taskId);

            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var projectManagerId = roleManager.FindByName("Project Manager").Users.Select(u => u.UserId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //Generate notification for urgentnote to project manager
                if (UrgentNotes.Length>1)
                {
                    string message = UrgentNotes +"from"+ task.User.UserName;
                    Notification.GenerateNotification(message,projectManagerId);
                }
                //end//
                task.percentageCompleted = percentageCompleted;
                if (task.percentageCompleted==100)
                {
                    task.IsCompleted = true;
                    string message = task.Name + " is completed by "+task.User.UserName ;
                    Notification.GenerateNotification(message, projectManagerId);

                }

                task.UrgenNotes.Add(UrgentNotes);
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAllTasks",new { id=task.UserId});
            }
            return View(task); 
        }
    }
}
