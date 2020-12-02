using Academic.Entities;

namespace Academic.Models
{
    public class LoginStudent
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public string Mail { get; set; }
        public string NrMatricol { get; set; }
        public string Cup { get; set; }
        public int? IdFormatie { get; set; }
        public int? IdSpecializare { get; set; }
        public string Token { get; set; }
        public LoginStudent(Student student, string token)
        {
            Id = student.IdUser;
            Nume = student.Nume;
            Prenume = student.Prenume;
            Username = student.Username;
            Token = token;
            NrMatricol = student.NrMatricol;
            Cup = student.Cup;
            TipUtilizator = student.TipUtilizator;
            IdFormatie = student.IdFormatie;
            IdSpecializare = student.IdSpecializare;
            Mail = student.Mail;
            Cnp = student.Cnp;
        }
    }
}