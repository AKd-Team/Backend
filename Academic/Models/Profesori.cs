using System.Linq;
using Academic.Entities;

namespace Academic.Models
{
    /*
     * Acest model este definit pentru a returna o entitate de tip profesor cu atribute mai detaliate.
     * In mod normal entitatea nu ar avea acces la date precum numele specializarii, din moment ce in baza de date
     * tabela contine doar id-uri pt tabelele cu care se leaga.
     */
    public class Profesori
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string TipUtilizator { get; set; }
        public string Mail { get; set; }
        public string Grad { get; set; }
        public string Departament { get; set; }
        public string Facultate { get; set; }
        public string Site { get; set; }
        
        public Profesori(Profesor profesor, string departament, string facultate)
        {
            Id = profesor.IdUser;
            Nume = profesor.Nume;
            Prenume = profesor.Prenume;
            Username = profesor.Username;
            TipUtilizator = profesor.TipUtilizator;
            Site = profesor.Site;
            Departament = departament;
            Facultate = facultate;
            Grad = profesor.Grad;
            Mail = profesor.Mail;
            Cnp = profesor.Cnp;
        }
    }
}