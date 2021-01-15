using System.ComponentModel.DataAnnotations;
using Academic.Entities;

namespace Academic.Models
{
    public class AdaugareNota
    {
        [Required] public int idStudent { get; set; }
        [Required] public int idMaterie { get; set; }
        [Required] public int? nota { get; set; }
        [Required] public bool restanta { get; set; }
    }
}