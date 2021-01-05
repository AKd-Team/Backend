namespace Academic.Models
{
    public class OptiuniReview
    {
        public int IdMaterie { get; set; }
        public string NumeMaterie { get; set; }
        public int AnDeStudiu { get; set; }
        public string AnCalendaristic { get; set; }
        public bool Curs { get; set; }
        public bool Seminar { get; set; }
        public bool Laborator { get; set; }

        public OptiuniReview(int idMaterie, string numeMaterie, int anDeStudiu, string anCalendaristic,bool curs, 
            bool seminar, bool laborator)
        {
            IdMaterie = idMaterie;
            NumeMaterie = numeMaterie;
            AnDeStudiu = anDeStudiu;
            AnCalendaristic = anCalendaristic;
            Curs = curs;
            Seminar = seminar;
            Laborator = laborator;
        }
    }
}