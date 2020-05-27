using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class ProjectsController : Controller
    {
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
    }
}