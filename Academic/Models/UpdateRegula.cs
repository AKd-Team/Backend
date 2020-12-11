using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class UpdateRegula
    {
        [Required] 
        public int IdRegula { get; set; }

        [Required] 
        public string Titlu { get; set; }
        
        [Required] 
        public string Continut { get; set; }
        
        [Required] 
        public int? IdFacultate { get; set; }
    }
}