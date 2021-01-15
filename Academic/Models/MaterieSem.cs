using System.ComponentModel.DataAnnotations;

namespace Academic.Models
{
    public class MaterieSem
    {
       [Required] public int IdMaterie { get; set; }
       
       [Required]  public string Nume { get; set; }
       
       [Required] public string Cod { get; set; }
        
       [Required]  public int? NrCredite { get; set; }
        
       [Required]  public string Descriere { get; set; }
        
       [Required]  public string Finalizare { get; set; }
        
       [Required]  public int? NrPachet { get; set; }
        
       [Required]  public int? TipActivitate { get; set; }



       public MaterieSem(int idmaterie, string nume, string cod, int? nrCredite, string descriere, string finalizare,
           int? nrpachet, int? tipactivitate)
       {
           IdMaterie = idmaterie;
           Nume = nume;
           Cod = cod;
           NrCredite = nrCredite;
           Descriere = descriere;
           Finalizare = finalizare;
           NrPachet = nrpachet;
           TipActivitate = tipactivitate;
       }
       public MaterieSem(){}
    }
    
}