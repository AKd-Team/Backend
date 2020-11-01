using System;
using System.Collections.Generic;
using System.Collections;

namespace Academic.Entities
{
    public partial class Users
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public BitArray PHash { get; set; }
        public BitArray PSalt { get; set; }
    }
}
