using System;

namespace Academic.Models
{
    public class OrarPersonalizat
    {
        public string Titlu { get; set;  }
        public string OraInceput { get; set; }
        public string OraSfarsit { get; set; }
        public string ZiuaSaptamanii { get; set; }
        public string Formatie { get; set; }
        public string NumeProfesor { get; set; }
        public string NumeSala { get; set; }
        public string Frecventa { get; set; }

        public OrarPersonalizat(string titlu, string oraInceput, string oraSfarsit, string ziuaSaptamanii, 
            string formatie, string numeProfesor, string numeSala, string frecventa)
        {
            Titlu = titlu;
            OraInceput = oraInceput;
            OraSfarsit = oraSfarsit;
            ZiuaSaptamanii = ziuaSaptamanii;
            Formatie = formatie;
            NumeProfesor = numeProfesor;
            NumeSala = numeSala;
            Frecventa = frecventa;
        }
    }
}