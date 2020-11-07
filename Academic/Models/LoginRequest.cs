using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class LoginRequest
    {
        //modelul alocat user-ului pe care se face loginrequest-ul
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}