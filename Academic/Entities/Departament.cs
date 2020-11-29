using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Departament
    {
        public Departament()
        {
            Profesor = new HashSet<Profesor>();
        }

        public int IdDepartament { get; set; }
        public string Nume { get; set; }
        public int? IdFacultate { get; set; }

        public virtual Facultate IdFacultateNavigation { get; set; }
        public virtual ICollection<Profesor> Profesor { get; set; }
    }
}
