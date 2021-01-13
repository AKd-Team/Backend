namespace Academic.Models
{
    public class SpecializariAdmin
    {
        public int IdSpecializare { get; set; }
        public string Nume { get; set; }
        public string Cod { get; set; }

        public SpecializariAdmin(int idspec, string nume, string cod)
        {
            IdSpecializare = idspec;
            Nume = nume;
            Cod = cod;
        }
    }
    
}