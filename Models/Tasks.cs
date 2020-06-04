using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem.Models
{
    public class Tasks
    {
        public Tasks(){
            UrgenNotes = new List<string>();
            }
        public Priority? Priority { get; set; }
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false
            ,ErrorMessage ="Name Required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Please add descrition")]
        public string Description { get; set; }
        public int? percentageCompleted { get; set; }
        public bool? IsCompleted { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SubmissionDate { get; set; }
        public List<string> UrgenNotes { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}