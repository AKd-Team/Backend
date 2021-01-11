namespace Academic.Models
{
    public class NotaStudenti
    {
        public int Nota { get; set; }
        public int NrStudenti { get; set; }

        public NotaStudenti(int nota)
        {
            Nota = nota;
            NrStudenti = 0;
        }

        public void updateStudenti(int nota)
        {
            NrStudenti++;
        }
    }
}