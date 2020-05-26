﻿using System;
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
		public string Description { get; set; }
		public int? percentageCompleted { get; set; }
		public bool? IsCompleted { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
		public int UserId { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}