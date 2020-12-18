namespace Academic.Models
{
    public class OrarExamen
    {
        public string Titlu { get; }
        public string OraInceput { get; }
        public string OraSfarsit { get; }
        public string Data { get; }
        
        public OrarExamen(string titlu, string oraInceput, string oraSfarsit, string data)
        {
            Titlu = titlu;
            OraInceput = oraInceput;
            OraSfarsit = oraSfarsit;
            Data = data;
        }

    }
}