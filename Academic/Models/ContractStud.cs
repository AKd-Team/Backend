namespace Academic.Models
{
    public class ContractStud
    {
       public string Cod { set; get; }
       public string Nume { set; get; }
       public bool Promovat { set; get; }

       public ContractStud(string cod, string nume, bool promovat)
       {
           Cod = cod;
           Nume = nume;
           Promovat = promovat;
       }
    }
}