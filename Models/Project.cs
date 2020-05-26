using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Project
    {
        public Project()
        {
            this.Tasks = new HashSet<Task>();
            this.ProjectUsers = new HashSet<ProjectUser>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public string Details { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}