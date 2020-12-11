﻿using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class AddRegula
    {
        [Required] 
        public string Titlu { get; set; }
        
        [Required] 
        public string Continut { get; set; }
        
        public int? IdFacultate { get; set; }
    }
}