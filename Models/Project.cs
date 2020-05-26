using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Project
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public string Details { get; set; }
    }
}