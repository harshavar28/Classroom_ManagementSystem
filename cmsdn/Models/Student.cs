using System.ComponentModel.DataAnnotations;

namespace cmsdn.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public string Department { get; set; } // Add this property
    }
}
