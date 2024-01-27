using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Saying you want to pick the ID not database remove if not needed
        public int ID { get; set; } 
        public string Name { get; set; }    
        public int QuantityRecieved {  get; set; }
        public int QuantityDefect {  get; set; }
        public string Description { get; set; }

        //for one to one relationship  with NCR
        public NCR NCR { get; set; }

        public ICollection<DefectList> DefectLists { get; set; } = new HashSet<DefectList>();
        public ICollection<Media> Medias { get; set; } = new HashSet<Media>(); //might not need since we have productdocumentmedias below. 

        [Display(Name="Product Images")]
        public ICollection<ProductDocumentMedia> ProductDocumentMedias { get; set; } = new HashSet<ProductDocumentMedia>();
    }
}
