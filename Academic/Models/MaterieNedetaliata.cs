using System;
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
        public override bool Equals(Object  obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || GetType() != obj.GetType())
            {
                return false;
            }
            MaterieNedetaliata m = (MaterieNedetaliata) obj;
            return (IdMaterie == m.IdMaterie);
        }
        public override int GetHashCode()
        {
            return (IdMaterie << 2);
        }

    }
}