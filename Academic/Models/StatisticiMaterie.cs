using System;
using System.Collections.Generic;

namespace Academic.Models
{
    public class StatisticiMaterie
    {
        public List<NotaStudenti> StatisticiNote { get; set; }

        public StatisticiMaterie()
        {
            StatisticiNote = new List<NotaStudenti>();
            StatisticiNote.Add(new NotaStudenti(1));
            StatisticiNote.Add(new NotaStudenti(2));
            StatisticiNote.Add(new NotaStudenti(3));
            StatisticiNote.Add(new NotaStudenti(4));
            StatisticiNote.Add(new NotaStudenti(5));
            StatisticiNote.Add(new NotaStudenti(6));
            StatisticiNote.Add(new NotaStudenti(7));
            StatisticiNote.Add(new NotaStudenti(8));
            StatisticiNote.Add(new NotaStudenti(9));
            StatisticiNote.Add(new NotaStudenti(10));
        }

        public void updateNrStudenti(int nota)
        {
            foreach (var notaStudenti in StatisticiNote)
            {
                if(notaStudenti.Nota == nota)
                    notaStudenti.updateStudenti(nota);
            }
        }
    }
}