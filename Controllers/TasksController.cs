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

        // GET: Tasks
        public ActionResult Index()
        {
            
            var tasks = db.Tasks.Include(t => t.Project).Include(t => t.User);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
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

        // GET: Tasks/Create
        public ActionResult Create(int projectId,string userId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.UserId = userId;
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, string description, int projectId, string userId,DateTime SubmissionDate,Priority priority)
        {
            if (ModelState.IsValid)
            {
                TaskHelper.Add(name, description, projectId, userId, SubmissionDate,priority);
            }
            return RedirectToAction("Info","Projects",new { id=projectId });
        }

        // GET: Tasks/Edit/5
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

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,percentageCompleted,IsCompleted,SubmissionDate,ProjectId,UserId")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", tasks.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", tasks.UserId);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
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

        // POST: Tasks/Delete/5
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
            
            if (ModelState.IsValid)
            {
                task.percentageCompleted = percentageCompleted;
                if (task.percentageCompleted==100)
                {
                    task.IsCompleted = true;
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
