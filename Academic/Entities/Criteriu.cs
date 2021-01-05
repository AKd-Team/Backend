using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Academic.Entities
{
    public partial class Criteriu
    {
        public Criteriu()
        {
            Review = new HashSet<Review>();
        }

        public int IdCriteriu { get; set; }
        public string Descriere { get; set; }

        [JsonIgnore]
        public virtual ICollection<Review> Review { get; set; }
    }
}
