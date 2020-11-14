﻿using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class RegisterRequest
    {
        [Required]
        public int IdUser { get; set; }
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
        public string Password { get; set; }
    }
}