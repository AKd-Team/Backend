using System;
using System.Collections.Generic;

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
        public string Mail { get; set; }
        public byte[] PHash { get; set; }
        public byte[] PSalt { get; set; }
        public string Grad { get; set; }
        public int? IdDepartament { get; set; }
        public string Site { get; set; }

        public virtual Departament IdDepartamentNavigation { get; set; }
        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
