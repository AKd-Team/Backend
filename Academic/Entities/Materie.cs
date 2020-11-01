using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Materie
    {
        public Materie()
        {
            Detaliucontract = new HashSet<Detaliucontract>();
            Orarmaterie = new HashSet<Orarmaterie>();
        }

        public int IdMaterie { get; set; }
        public string Nume { get; set; }
        public string Cod { get; set; }
        public int? NrCredite { get; set; }
        public string Descriere { get; set; }
        public string Finalizare { get; set; }
        public int? NrPachet { get; set; }
        public int? TipActivitate { get; set; }

        public virtual ICollection<Detaliucontract> Detaliucontract { get; set; }
        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
    }
}
