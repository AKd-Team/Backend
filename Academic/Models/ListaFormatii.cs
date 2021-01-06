namespace Academic.Models
{
    public class ListaFormatii
    {
        public int idFormatie { get; set; }
        public string Grupa { get; set; }
        
        public ListaFormatii(int id, string grupa)
        {
            idFormatie = id;
            Grupa = grupa;
        }
    }
   
}