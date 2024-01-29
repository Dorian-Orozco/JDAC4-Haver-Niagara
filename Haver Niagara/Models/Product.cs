using System.ComponentModel.DataAnnotations;


namespace Haver_Niagara.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; } 
        public string Name { get; set; }

        public int ProductNumber { get; set; }
        public int QuantityRecieved {  get; set; }
        public int QuantityDefect {  get; set; }
        public string Description { get; set; }

        //navigation property for supplier 
        public Supplier Supplier { get; set; }

        //One to many with NCR
        public ICollection<NCR> NCRs { get; set; } = new HashSet<NCR>();

        public ICollection<DefectList> DefectLists { get; set; } = new HashSet<DefectList>();
        public ICollection<Media> Medias { get; set; } = new HashSet<Media>(); //might not need since we have productdocumentmedias below. 

        //[Display(Name="Product Images")]
        //public ICollection<ProductDocumentMedia> ProductDocumentMedias { get; set; } = new HashSet<ProductDocumentMedia>();

    }
}
