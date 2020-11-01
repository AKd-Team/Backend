using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Orarmaterie
    {
        public TimeSpan OraInceput { get; set; }
        public TimeSpan OraSfarsit { get; set; }
        public int IdMaterie { get; set; }
        public int IdProfesor { get; set; }
        public string ZiuaSaptamanii { get; set; }
        public string Frecventa { get; set; }
        public int IdFormatie { get; set; }
        public int IdSpecializare { get; set; }
        public int IdSala { get; set; }
        public DateTime? Data { get; set; }
        public string Tip { get; set; }

        public virtual Formatie Id { get; set; }
        public virtual Materie IdMaterieNavigation { get; set; }
        public virtual Profesor IdProfesorNavigation { get; set; }
        public virtual Sala IdSalaNavigation { get; set; }
    }
}
