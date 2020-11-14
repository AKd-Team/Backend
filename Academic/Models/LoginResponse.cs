using Academic.Entities;

namespace Academic.Models
{
    public class LoginResponse
    {
        //modelul ce returneaza utilizatorul, dupa ce s-a facut request-ul
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string TipUtilizator { get; set; }


        public LoginResponse(Users user, string token)
        {
            Id = user.IdUser;
            FirstName = user.Nume;
            LastName = user.Prenume;
            Username = user.Username;
            Token = token;
            TipUtilizator = user.TipUtilizator;
        }
    }
}
