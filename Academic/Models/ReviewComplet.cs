using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class ReviewComplet
    {
        [Required]
        public int IdProfesor { get; set; }
        [Required]
        public int IdMaterie { get; set; }
        [Required]
        public int IdStudent { get; set; }
        [Required]
        public int AnDeStudiu { get; set; }
        [Required]
        public string AnCalendaristic { get; set; }
        public List<int> Criterii { get; set; }
        public List<int> Note { get; set; } // Fiecare nota e pentru criteriul de pe pozitia echivalenta in Criterii
    }
}