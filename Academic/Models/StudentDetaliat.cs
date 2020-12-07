using Academic.Entities;

namespace Academic.Models
{    /* Model folosit pentru extragerea detaliata a datelor personale ale unui student
      * Include atribute din tabela specializare si facultate
      */
    public class StudentDetaliat
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Cnp { get; set; }
        public string Mail { get; set; }
        public string NrMatricol { get; set; }
        public string Cup { get; set; }
        public string Grupa { get; set; }
        public string Semigrupa { get; set; }
        public string An_studiu { get; set; }
        public string Facultate { get; set; }
        public string Nivel { get; set; }
        public string Specializare { get; set; }
        
        

       public StudentDetaliat(Student student, string grupa, string semigrupa, string an_studiu, string facultate, string nivel, string specializare)
       {
           Nume = student.Nume;
           Prenume = student.Prenume;
           Cnp = student.Cnp;
           Mail = student.Mail;
           NrMatricol = student.NrMatricol;
           Cup = student.Cup;
           Grupa = grupa;
           Semigrupa = semigrupa;
           An_studiu = an_studiu;
           Facultate = facultate;
           Nivel = nivel;
           Specializare = specializare;

       }
    }
}