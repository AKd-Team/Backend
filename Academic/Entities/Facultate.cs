using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Facultate
    {
        public Facultate()
        {
            Profesor = new HashSet<Profesor>();
            Specializare = new HashSet<Specializare>();
        }

        public int IdFacultate { get; set; }
        public string Nume { get; set; }

        public virtual ICollection<Profesor> Profesor { get; set; }
        public virtual ICollection<Specializare> Specializare { get; set; }
    }
}
