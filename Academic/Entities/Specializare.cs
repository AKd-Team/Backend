using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Specializare
    {
        public Specializare()
        {
            Admin = new HashSet<Admin>();
            Formatie = new HashSet<Formatie>();
            MaterieSpecializare = new HashSet<MaterieSpecializare>();
        }

        public int IdSpecializare { get; set; }
        public string Nume { get; set; }
        public string Cod { get; set; }
        public int? IdFacultate { get; set; }
        public string Nivel { get; set; }

        public virtual Facultate IdFacultateNavigation { get; set; }
        public virtual ICollection<Admin> Admin { get; set; }
        public virtual ICollection<Formatie> Formatie { get; set; }
        public virtual ICollection<MaterieSpecializare> MaterieSpecializare { get; set; }
    }
}
