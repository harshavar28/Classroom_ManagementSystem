using System.ComponentModel.DataAnnotations;

namespace cmsdn.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string ClassTeacherOf { get; set; }

    }
}
