using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class AddMaterie
    {
        [Required] public string Nume { get; set; }
        
        
        [Required]public string Cod { get; set; }
       
        [Required] public int? NrCredite { get; set; }
       
        [Required] public string Descriere { get; set; }
       
        [Required] public string Finalizare { get; set; }
       
        [Required] public int? NrPachet { get; set; }
       
        [Required] public int? TipActivitate { get; set; }
    }
}