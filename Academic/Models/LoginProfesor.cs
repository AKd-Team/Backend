using Academic.Entities;

namespace Academic.Models
{
    public class LoginProfesor
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public string Mail { get; set; }
        public string Grad { get; set; }
        public int? IdDepartament { get; set; }
        public string Site { get; set; }
        public string Token { get; set; }
        public LoginProfesor(Profesor profesor, string token)
        {
            Id = profesor.IdUser;
            Nume = profesor.Nume;
            Prenume = profesor.Prenume;
            Username = profesor.Username;
            Token = token;
            TipUtilizator = profesor.TipUtilizator;
            Site = profesor.Site;
            IdDepartament = profesor.IdDepartament;
            Grad = profesor.Grad;
            Mail = profesor.Mail;
            Cnp = profesor.Cnp;
        }
    }
}