using System;
using System.Collections.Generic;

namespace Academic.Models
{
    public class StatisticiMaterie
    {
        public Dictionary<int, int> StatisticiNote { get; set; }

        public StatisticiMaterie()
        {
            StatisticiNote = new Dictionary<int, int>();
            StatisticiNote.Add(1, 0);
            StatisticiNote.Add(2, 0);
            StatisticiNote.Add(3, 0);
            StatisticiNote.Add(4, 0);
            StatisticiNote.Add(5, 0);
            StatisticiNote.Add(6, 0);
            StatisticiNote.Add(7, 0);
            StatisticiNote.Add(8, 0);
            StatisticiNote.Add(9, 0);
            StatisticiNote.Add(10, 0);
        }

        public void updateNrStudenti(int nota)
        {
            StatisticiNote[nota]++;
        }
    }
}