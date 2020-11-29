using System;
using System.Collections.Generic;

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
        public string Mail { get; set; }
        public byte[] PHash { get; set; }
        public byte[] PSalt { get; set; }
    }
}
