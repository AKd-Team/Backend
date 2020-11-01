using System;
using System.Collections.Generic;

namespace Academic.Entities
{
    public partial class Contractdestudiu
    {
        public Contractdestudiu()
        {
            Detaliucontract = new HashSet<Detaliucontract>();
        }

        public int IdStudent { get; set; }
        public int AnDeStudiu { get; set; }
        public string AnCalendaristic { get; set; }
        public string Cod { get; set; }

        public virtual Student IdStudentNavigation { get; set; }
        public virtual ICollection<Detaliucontract> Detaliucontract { get; set; }
    }
}
