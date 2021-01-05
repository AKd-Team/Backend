using Academic.Entities;

namespace Academic.Models
{
    public class AdaugareNota
    {
        public int idStudent { get; set; }
        public int idMaterie { get; set; }
        public int? nota { get; set; }
        public bool restanta { get; set; }

        public AdaugareNota(Detaliucontract dc,bool isNotaRestanta)
        {
            idStudent = dc.IdStudent;
            idMaterie = dc.IdMaterie;
            nota = dc.Nota;
            restanta = isNotaRestanta;
        }
    }
}