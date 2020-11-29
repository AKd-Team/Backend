using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class MaterieSpecializare
    {
        public int IdSpecializare { get; set; }
        public int IdMaterie { get; set; }
        public int? Semestru { get; set; }

        public virtual Materie IdMaterieNavigation { get; set; }
        public virtual Specializare IdSpecializareNavigation { get; set; }
    }
}
