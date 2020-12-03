using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Academic.Entities
{
    public partial class Profesor
    {
        public Profesor()
        {
            Orarmaterie = new HashSet<Orarmaterie>();
            Review = new HashSet<Review>();
        }
        [JsonIgnore]
        public int IdUser { get; set; }
        [JsonIgnore]
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        [JsonIgnore]
        public string Cnp { get; set; }
        [JsonIgnore]
        public string TipUtilizator { get; set; }
        public string Mail { get; set; }
        [JsonIgnore]
        public byte[] PHash { get; set; }
        [JsonIgnore]
        public byte[] PSalt { get; set; }
        public string Grad { get; set; }
        public int? IdDepartament { get; set; }
        public string Site { get; set; }
        [JsonIgnore]
        public virtual Departament IdDepartamentNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Orarmaterie> Orarmaterie { get; set; }
        [JsonIgnore]
        public virtual ICollection<Review> Review { get; set; }
    }
}
