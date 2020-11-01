using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Sala
    {
        public Sala()
        {
            Orarmaterie = new HashSet<Orarmaterie>();
        }

        public int IdSala { get; set; }
        public string Nume { get; set; }
        public string Locatie { get; set; }

        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
    }
}
