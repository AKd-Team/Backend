using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class RegisterStudent
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Nume { get; set; }
        [Required]
        public string Prenume { get; set; }
        [Required]
        public string Cnp { get; set; }
        [Required]
        public string TipUtilizator { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NrMatricol { get; set; }
        [Required]
        public string Cup { get; set; }
        [Required]
        public int? IdFormatie { get; set; }
        [Required]
        public int? IdSpecializare { get; set; }
    }
}