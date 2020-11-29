using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Facultate
    {
        public Facultate()
        {
            Departament = new HashSet<Departament>();
            Regulament = new HashSet<Regulament>();
            Specializare = new HashSet<Specializare>();
        }

        public int IdFacultate { get; set; }
        public string Nume { get; set; }

        public virtual ICollection<Departament> Departament { get; set; }
        public virtual ICollection<Regulament> Regulament { get; set; }
        public virtual ICollection<Specializare> Specializare { get; set; }
    }
}
