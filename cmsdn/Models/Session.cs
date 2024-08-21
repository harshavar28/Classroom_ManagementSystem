using System;
using System.ComponentModel.DataAnnotations;

namespace cmsdn.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; } // Ensure this is TimeSpan
        public string TeacherName { get; set; }
        public string Subject { get; set; } // Updated from Department
        public string Class { get; set; }
    }

}
