using System;
using System.Collections.Generic;
using System.Collections;

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
        public BitArray PHash { get; set; }
        public BitArray PSalt { get; set; }
        public int IdStudent { get; set; }
        public string NrMatricol { get; set; }
        public string Cup { get; set; }
        public int? IdFormatie { get; set; }
        public int? IdSpecializare { get; set; }

        public virtual Formatie Id { get; set; }
        public virtual ICollection<Contractdestudiu> Contractdestudiu { get; set; }
    }
}
