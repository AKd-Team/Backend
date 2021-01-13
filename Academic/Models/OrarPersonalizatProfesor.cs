namespace Academic.Models
{
    public class OrarPersonalizatProfesor
    {
        public string NumeMaterie { get; set;  }
        public string OraInceput { get; set; }
        public string OraSfarsit { get; set; }
        public string ZiuaSaptamanii { get; set; }
        public string Formatie { get; set; }
        public string NumeSala { get; set; }
        public string Frecventa { get; set; }

        public OrarPersonalizatProfesor(string numeMaterie, string oraInceput, string oraSfarsit, string ziuaSaptamanii, 
            string formatie, string numeSala, string frecventa)
        {
            NumeMaterie = numeMaterie;
            OraInceput = oraInceput;
            OraSfarsit = oraSfarsit;
            ZiuaSaptamanii = ziuaSaptamanii;
            Formatie = formatie;
            NumeSala = numeSala;
            Frecventa = frecventa;
        }
    }
}