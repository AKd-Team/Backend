using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Detaliucontract
    {
        public Detaliucontract()
        {
            Review = new HashSet<Review>();
        }

        public int IdMaterie { get; set; }
        public int IdStudent { get; set; }
        public int AnDeStudiu { get; set; }
        public string AnCalendaristic { get; set; }
        public int? Nota { get; set; }
        public int? NotaRestanta { get; set; }
        public bool? Promovata { get; set; }
        public DateTime? DataPromovarii { get; set; }
        public DateTime? DataExamen { get; set; }
        public DateTime? DataRestanta { get; set; }
        public int? Semestru { get; set; }

        public virtual Contractdestudiu Contractdestudiu { get; set; }
        public virtual Materie IdMaterieNavigation { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
