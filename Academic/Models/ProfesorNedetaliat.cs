using System;
using Academic.Entities;

namespace Academic.Models
{
    public class ProfesorNedetaliat
    {
        public int IdProfesor { get; set; }
        public string NumeProfesor { get; set; }

        public ProfesorNedetaliat(int idProfesor, string numeProfesor)
        {
            IdProfesor = idProfesor;
            NumeProfesor = numeProfesor;
        }

        public override bool Equals(Object  obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || GetType() != obj.GetType())
            {
                return false;
            }
            ProfesorNedetaliat m = (ProfesorNedetaliat) obj;
            return (IdProfesor == m.IdProfesor);
        }
        public override int GetHashCode()
        {
            return (IdProfesor << 2);
        }
    }
}