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
		public static bool Add(string description, int projectId, string userId)
		{
			// create a new task object
			Tasks newTask = new Tasks();

			// add all the data inside
			newTask.Description = description;
			newTask.ProjectId = projectId;
			
			var project = db.Projects.FirstOrDefault(p => p.Id == projectId);
			newTask.Project = project;
			
			newTask.UserId = userId;

			var user = db.Users.FirstOrDefault(u => u.Id == userId);
			newTask.User = user;

			// put it in the database
			project.Tasks.Add(newTask);
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

		// Assign a task to a number of Developers
		public static bool AssignTaskToDeveloper(Tasks task, string developerId)
		{
			// find the task inside the db
			var result = db.Projects.Where(p => p.Tasks.Contains(task)).Select(pr => pr.Tasks.FirstOrDefault(tsk => tsk.Id == task.Id)).First();

			// from the user's side, add the side
			var user = db.Users.FirstOrDefault(u => u.Id == developerId);

			user.Tasks.Add(result);

			db.SaveChanges();
			return true;
		}
	}
}