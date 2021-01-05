using System.Collections.Generic;

namespace Academic.Models
{
    public class RezultatEvaluare
    {
        public List<string> Criterii { get; }
        public List<double?> Medii { get; }

        public RezultatEvaluare()
        {
            Criterii = new List<string>();
            Medii = new List<double?>();
        }

        public void AddElement(string criteriu, double? medie)
        {
            Criterii.Add(criteriu);
            Medii.Add(medie);
        }
    }
}