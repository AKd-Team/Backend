using Academic.Entities;

namespace Academic.Models
{
    public class DateStudentPtProfesor
    {
        public int IdStudent { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Grupa { get; set; }
        public string Semigrupa { get; set; }
        public string Specializare { get; set; }
        public string Facultate { get; set; }

        public DateStudentPtProfesor(Student student, string grupa, string semigrupa, string specializare, string facultate)
        {
            IdStudent = student.IdUser;
            Nume = student.Nume;
            Prenume = student.Prenume;
            Grupa = grupa;
            Semigrupa = semigrupa;
            Specializare = specializare;
            Facultate = facultate;

        }
    }
}