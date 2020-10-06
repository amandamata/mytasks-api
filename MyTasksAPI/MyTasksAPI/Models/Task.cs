using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTasks.Models
{
    public class Task
    {
        [Key]
        public int IdTaskApi { get; set; }
        public int IdTaskApp { get; set; }
        public string Title { get; set; }
        public DateTime HourDate { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public bool Excluded { get; set; }

        [ForeignKey("User")]
        public string IdUser { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
