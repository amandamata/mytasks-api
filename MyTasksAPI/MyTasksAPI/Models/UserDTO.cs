using System.ComponentModel.DataAnnotations;

namespace MyTasksAPI.Models
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PersistPassword { get; set; }
    }
}
