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
		public static bool AddProject(string name, string customerName, DateTime? deadline,Priority priority)
		{
			Project project = new Project() { Name = name, Customer = customerName, Deadline = deadline,Priority=priority };
			
				db.Projects.Add(project);
				db.SaveChanges();
				return true;
		}

		// Delete() for deleting projects from the database
		public static bool Delete(int id)
		{
			var project = db.Projects.Find(id);
			
			db.Projects.Remove(project);
			db.SaveChanges();
			return true;
		
		}

		// Update() for Updating Projects to the database
		public static bool Update()
		{
			return true;

		}

		
	}
}