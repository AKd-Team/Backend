using System;
using System.Collections.Generic;
using System.Collections;

namespace Academic.Entities
{
    public partial class Admin
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public byte[] PHash { get; set; }
        public byte[] PSalt { get; set; }
        public int IdAdmin { get; set; }
        public string Mail { get; set; }
        public int? IdSpecializare { get; set; }

        public virtual Specializare IdSpecializareNavigation { get; set; }
    }
}
