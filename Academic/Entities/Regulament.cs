using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Regulament
    {
        public int IdRegulament { get; set; }
        public string Titlu { get; set; }
        public string Continut { get; set; }
        public int? IdFacultate { get; set; }

        public virtual Facultate IdFacultateNavigation { get; set; }
    }
}
