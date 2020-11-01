using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}