using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
	public enum Priority
	{
		Urgent,
		High,
		Medium,
		Low,
	}
	public class Project
	{
		public Project()
		{
			this.Tasks = new HashSet<Tasks>();
			this.ProjectUsers = new HashSet<ProjectUser>();
		}
		public Priority? Priority { get; set; }

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[DisplayName("DeadLine")]
		[DataType(DataType.Date)]

		public DateTime? Deadline { get; set; }

		public string Customer { get; set; }

		public double Budget { get; set; }

		public ICollection<Tasks> Tasks { get; set; }

		public ICollection<ProjectUser> ProjectUsers { get; set; }

		public void PassedDeadlineWithAnyUnFinishedTask()
		{
			if ((Deadline.Value.Date-DateTime.Now.Date).Equals(-1) && Tasks.Any(t => t.IsCompleted == false))
			{
				ApplicationDbContext db = new ApplicationDbContext();

				var roleStore = new RoleStore<IdentityRole>(db);
				var roleManager = new RoleManager<IdentityRole>(roleStore);

				var projectManagerId = roleManager.FindByName("Project Manager").Users.Select(u => u.UserId).FirstOrDefault();

				string message = Name + "has a unfinished task but passed the deadline";
				Notification.GenerateNotification(message, projectManagerId);
			}
		}
	}
}