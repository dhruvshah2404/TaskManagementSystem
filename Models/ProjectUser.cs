using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
	public class ProjectUser
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
		[ForeignKey("User")]
		public string User_Id { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}