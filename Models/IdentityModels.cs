﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskManagementSystem.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser()
		{
			ProjectUsers = new HashSet<ProjectUser>();
			Tasks = new HashSet<Tasks>();
			Notifications = new HashSet<Notification>();
		}
		public ICollection<ProjectUser> ProjectUsers { get; set; }
		public ICollection<Tasks> Tasks { get; set; }
		public ICollection<Notification> Notifications { get; set; }
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("connection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
		public DbSet<Project> Projects { get; set; }

		public DbSet<Tasks> Tasks { get; set; }

		public DbSet<Notification> Notifications { get; set; }

		public DbSet<CompletedTaskModel> CompletedTasks { get; set; }

		public DbSet<ProjectUser> ProjectUsers { get; set; }

	}
}