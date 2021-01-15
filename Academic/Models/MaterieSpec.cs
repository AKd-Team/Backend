using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class MaterieSpec
    {
       [Required] public int IdSpecializare { get; set; }
       
       [Required] public int IdMaterie { get; set; }
       public int? Semestru { get; set; }
    }
}