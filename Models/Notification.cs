using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Models
{

	public enum NotificationStatus
	{
		Read,
		Unread
	}

	// A developer receives a notification when the task is one day to pass before the deadline
	// the pm received notif when 
	//task of project is completed, 
	//when project passed a deadline with unfinished tasks

	public class Notification
	{

		public int Id { get; set; }
		public string Message { get; set; }
		public NotificationStatus Status { get; set; }
		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }

		public static void GenerateNotification(string message, string userId)
		{   
			ApplicationDbContext db = new ApplicationDbContext();

		//PM - Task or project completed
		//PM - 
			var notification = new Notification()
			{
				Message = message,
				UserId = userId,
				Status = NotificationStatus.Unread
			};
			db.Notifications.Add(notification);
			db.SaveChanges();
		}

	}
}