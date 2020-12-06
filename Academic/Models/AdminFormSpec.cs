using System.Collections.Generic;
using Academic.Entities;

namespace Academic.Models
{
    /*
     * Acest model are drept menire returnarea unei specializari cu toate formatiile sale
     */
    public class AdminFormSpec
    {
        public string Specializare { get; set; }
        public List<string> Grupe { get; set; }
        public List<string> SemiGrupe { get; set; }

        public AdminFormSpec(string specializare, List<string> grupe, List<string> semiGrupe)
        {
            Specializare = specializare;
            Grupe = grupe;
            SemiGrupe = semiGrupe;
        }
    }
}