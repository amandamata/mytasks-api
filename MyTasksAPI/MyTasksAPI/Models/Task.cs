using System;

namespace MyTasksAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime HourDate { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }

    }
}
