using Npgsql.TypeHandlers.NumericHandlers;

namespace Academic.Models
{
    public class StudentFaraNota
    {
        public string Nume { get; set; }

        public string Prenume { get; set; }

        public string Grupa { get; set; }

        public string Specializare { get; set; }

        public int idStudent { get; set; }

        public StudentFaraNota(string nume, string prenume, string grupa, string specializare, int id_student)
        {
            Nume = nume;
            Prenume = prenume;
            Grupa = grupa;
            Specializare = specializare;
            idStudent = id_student;
        }
        
    }

}