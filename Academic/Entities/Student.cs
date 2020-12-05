using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Academic.Entities
{
    public partial class Student
    {
        public Student()
        {
            Contractdestudiu = new HashSet<Contractdestudiu>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public string Mail { get; set; }
        [JsonIgnore]
        public byte[] PHash { get; set; }
        [JsonIgnore]
        public byte[] PSalt { get; set; }
        public string NrMatricol { get; set; }
        public string Cup { get; set; }
        public int? IdFormatie { get; set; }
        public int? IdSpecializare { get; set; }
        [JsonIgnore]
        public virtual Formatie Id { get; set; }
        [JsonIgnore]
        public virtual ICollection<Contractdestudiu> Contractdestudiu { get; set; }
    }
}
