using System.ComponentModel.DataAnnotations;

namespace cmsdn.Models
{
    public class Password
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CommonPassword { get; set; }
    }
}
