using System;
using System.Collections.Generic;
using System.Collections;

namespace Academic.Entities
{
    public partial class Profesor
    {
        public Profesor()
        {
            Orarmaterie = new HashSet<Orarmaterie>();
            Review = new HashSet<Review>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public byte[] PHash { get; set; }
        public byte[] PSalt { get; set; }
        public int IdProfesor { get; set; }
        public string Grad { get; set; }
        public string Domeniu { get; set; }
        public string Site { get; set; }
        public string Mail { get; set; }
        public int? IdFacultate { get; set; }

        public virtual Facultate IdFacultateNavigation { get; set; }
        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
