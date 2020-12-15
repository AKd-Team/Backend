using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
    }
}
