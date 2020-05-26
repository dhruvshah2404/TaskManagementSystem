using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
	public static class ProjectHelper
	{
		public static ApplicationDbContext db = new ApplicationDbContext();// the database

		// Add() for adding projects to the database
		public static bool Add ( Project project )
		{
			// db.Projects not working
			return true;
		}

		// Delete() for deleting projects from the database
		public static bool Delete ()
		{
			return true;
		}

		// Update() for Updating Projects to the database
		public static bool Update ()
		{
			return true;
		}

	}
}