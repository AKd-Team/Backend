using System.Collections.Generic;

namespace Academic.Models
{
    /*
     * Acest model e pentru a transmite facultatile impreuna cu o lista de departamente.
     */
    public class FacultatiCuDepartamente
    {
        public string NumeFacultate { get; set; }
        public List<string> Departamente { get; set; }

        public FacultatiCuDepartamente(string facultate, List<string> departamente)
        {
            NumeFacultate = facultate;
            Departamente = departamente;
        }
    }
}