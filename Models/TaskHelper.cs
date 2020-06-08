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
		public static bool Add(string name,string description, int projectId, string userId, DateTime submissionDate,Priority priority)
		{
			// create a new task object
			Tasks newTask = new Tasks() {Name=name,Description=description,ProjectId=projectId,UserId=userId,SubmissionDate=submissionDate,Priority=priority, IsCompleted = false,percentageCompleted=0,};

			// add all the data inside
		
			// put it in the database
			db.Tasks.Add(newTask);
			db.SaveChanges();

			return true;
		}

		// Delete Task
		public static bool Delete(int taskId)
		{
			// delete from the project
			var task = db.Tasks.Find(taskId);
			db.Tasks.Remove(task);
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