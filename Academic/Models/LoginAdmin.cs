using Academic.Entities;

namespace Academic.Models
{
    public class LoginAdmin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public string Mail { get; set; }
        public int? IdSpecializare { get; set; }
        public string Token { get; set; }

        public LoginAdmin(Admin admin, string token)
        {
            Id = admin.IdUser;
            Nume = admin.Nume;
            Prenume = admin.Prenume;
            Cnp = admin.Cnp;
            Mail = admin.Mail;
            IdSpecializare = admin.IdSpecializare;
            Username = admin.Username;
            Token = token;
            TipUtilizator = admin.TipUtilizator;
        }
    }
}