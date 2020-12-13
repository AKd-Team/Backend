using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Academic.Entities
{
    public partial class Materie
    {
        public Materie()
        {
            Detaliucontract = new HashSet<Detaliucontract>();
            MaterieSpecializare = new HashSet<MaterieSpecializare>();
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
        [JsonIgnore]
        public virtual ICollection<Detaliucontract> Detaliucontract { get; set; }
        [JsonIgnore]
        public virtual ICollection<MaterieSpecializare> MaterieSpecializare { get; set; }
        [JsonIgnore]
        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
    }
}
