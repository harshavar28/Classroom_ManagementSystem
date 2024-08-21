using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cmsdn.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Required]
        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public Session Session { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
