using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
	public class Tasks
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		public int? percentageCompleted { get; set; }
		public bool? IsCompleted { get; set; }
		[DataType(DataType.Date)]
		public DateTime? SubmissionDate { get; set; }
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}