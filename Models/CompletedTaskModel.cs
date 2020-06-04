using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class CompletedTaskModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string DeveloperName { get; set; }
        public string ProjectName { get; set; }
    }
}