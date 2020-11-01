using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Formatie
    {
        public Formatie()
        {
            Orarmaterie = new HashSet<Orarmaterie>();
            Student = new HashSet<Student>();
        }

        public int IdFormatie { get; set; }
        public int IdSpecializare { get; set; }
        public string Grupa { get; set; }
        public string SemiGrupa { get; set; }
        public string AnStudiu { get; set; }

        public virtual Specializare IdSpecializareNavigation { get; set; }
        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
