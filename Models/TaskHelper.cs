using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace TaskManagementSystem.Models
{
	public static class TaskHelper
	{
		static ApplicationDbContext db = new ApplicationDbContext();
		// Can only be accessed by Project Managers

		// Add Task
		public static bool Add(string name,string description, int projectId, string userId, DateTime submissionDate)
		{
			// create a new task object
			Tasks newTask = new Tasks() { IsCompleted = false,percentageCompleted=0};

			// add all the data inside
			newTask.Name = name;
			newTask.Description = description;
			newTask.ProjectId = projectId;
			newTask.UserId = userId;
			newTask.SubmissionDate = submissionDate;
			// put it in the database
			db.Tasks.Add(newTask);
			db.SaveChanges();

			return true;
		}

		// Delete Task
		public static bool Delete(int projectId,int taskId)
		{
			// delete from the project
			var project = db.Projects.FirstOrDefault(p => p.Id == projectId);
			//var project = db.Projects.FirstOrDefault(p => p.Tasks.Any(t => t.Id == taskId));
			var task = project.Tasks.FirstOrDefault(t => t.Id == taskId);
			project.Tasks.Remove(task);

			//  if any user contains the task, then delete it as well.
			var userContainingTask = db.Users.FirstOrDefault(u => u.Tasks.Contains(task));
			userContainingTask.Tasks.Remove(task);
			db.SaveChanges();

			return true;
		}

		// Update Task
		public static bool Update()
		{
			return true;
		}

	}
}