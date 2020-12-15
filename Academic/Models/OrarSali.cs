namespace Academic.Models
{
    public class OrarSali
    {
        public string Titlu { get; set; }
        public string OraInceput { get; set; }
        public string OraSfarsit { get; set; }
        public string Data { get; set; }

        public string CodSpecializare { get; set; }

        public OrarSali(string titlu, string oraInceput, string oraSfarsit, string data, string codSpecializare)
        {
            Titlu = titlu;
            OraInceput = oraInceput;
            OraSfarsit = oraSfarsit;
            Data = data;
            CodSpecializare = codSpecializare;
        }
    }
}