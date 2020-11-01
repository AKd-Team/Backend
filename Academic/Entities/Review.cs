using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Review
    {
        public int IdProfesor { get; set; }
        public int IdMaterie { get; set; }
        public int IdCriteriu { get; set; }
        public int IdStudent { get; set; }
        public int AnDeStudiu { get; set; }
        public string AnCaledaristic { get; set; }
        public int? Nota { get; set; }

        public virtual Detaliucontract Detaliucontract { get; set; }
        public virtual Criteriu IdCriteriuNavigation { get; set; }
        public virtual Profesor IdProfesorNavigation { get; set; }
    }
}
