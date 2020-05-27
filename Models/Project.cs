using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Project
    {
        public Project()
        {
            this.Tasks = new HashSet<Tasks>();
            this.ProjectUsers = new HashSet<ProjectUser>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("DeadLine")]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        public string CustomerName { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}