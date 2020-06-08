using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{

    public class NotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notifications
        public ActionResult Index(string id)
        {
            var notifications = db.Notifications.Where(n => n.UserId == id).ToList();
            return View(notifications);
        }
        //GET: INFO
        public ActionResult Info(int id)
        {
            var notification = db.Notifications.Find(id);
            notification.Status = NotificationStatus.Read;
            db.SaveChanges();
            return View(notification);
        }
 
    }
}