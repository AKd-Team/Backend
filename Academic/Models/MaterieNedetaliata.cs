using Academic.Entities;

namespace Academic.Models
{
    public class MaterieNedetaliata
    {
        public int IdMaterie { get; set; }
        public string Nume { get; set; }

        public MaterieNedetaliata(Materie materie)
        {
            IdMaterie = materie.IdMaterie;
            Nume = materie.Nume;
        }
    }
}