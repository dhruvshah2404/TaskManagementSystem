using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
	public class User
	{
		public User()
		{
			this.ProjectUsers = new HashSet<ProjectUser>();
			this.Tasks = new HashSet<Task>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<ProjectUser> ProjectUsers { get; set; }
		public ICollection<Task> Tasks { get; set; }

	}
}