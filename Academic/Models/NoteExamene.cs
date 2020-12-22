using Academic.Entities;

namespace Academic.Models
{
    public class NoteExamene
    {
        public int AnStudiu { get; set; }
        public int? SemestruPlan { get; set; }
        public string CodDisciplina { get; set; }
        public string Disciplina { get; set; }
        
        public int? NotaSesiune { get; set; }
        public int? NotaRestanta { get; set; }
        public int? NotaFinala { get; set; }
        public int? NrCredite { get; set; }
        public string DataPromovarii { get; set; }


        public NoteExamene(Detaliucontract detaliuContract,Materie materie,int? notaFinala)
        {
            AnStudiu = detaliuContract.AnDeStudiu;
            SemestruPlan = detaliuContract.Semestru;
            CodDisciplina = materie.Cod;
            Disciplina = materie.Nume;
            NotaSesiune = detaliuContract.Nota;
            NotaRestanta = detaliuContract.NotaRestanta;
            NotaFinala = notaFinala;
            NrCredite = materie.NrCredite;
            if (detaliuContract.DataPromovarii != null)
                DataPromovarii = detaliuContract.DataPromovarii.Value.ToString("yyyy-MM-dd");
            else DataPromovarii = "-";
        }
    }
}