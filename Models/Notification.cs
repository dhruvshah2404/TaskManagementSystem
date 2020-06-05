using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
		public int UserId { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}