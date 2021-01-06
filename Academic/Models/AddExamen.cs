using System;
using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class AddExamen
    {
        [Required] public string OraInceput { get; set; }
        
        [Required] public string OraSfarsit { get; set; }
        
        [Required] public int IdMaterie { get; set; }
        
        [Required] public int IdProfesor { get; set; }

        [Required] public int IdFormatie { get; set; }

        [Required] public int IdSala { get; set; }
        
        [Required] public DateTime Data { get; set; }
    }
    
}