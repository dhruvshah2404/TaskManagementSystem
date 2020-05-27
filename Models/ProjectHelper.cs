using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
	public static class ProjectHelper
	{
		static ApplicationDbContext db = new ApplicationDbContext();// the database

		// Add() for adding projects to the database
		public static bool AddProject(string name, string customerName, DateTime? deadline)
		{
			Project project = new Project() { Name = name, CustomerName = customerName, Deadline = deadline };
			
				db.Projects.Add(project);
				db.SaveChanges();
				return true;
		}

		// Delete() for deleting projects from the database
		public static bool Delete(string name)
		{
			var project = new Project() { Name = name };
			if (db.Projects.Contains(project))
			{
				db.Projects.Remove(project);
				db.SaveChanges();
				return true;
			}
			else
			{
				return false;// no project with this name
			}
		}

		// Update() for Updating Projects to the database
		public static bool Update()
		{
			return true;
		}

	}
}