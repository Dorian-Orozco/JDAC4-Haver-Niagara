using System.ComponentModel.DataAnnotations;


namespace Haver_Niagara.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Prod. Number")]
        public int ProductNumber { get; set; }

        [Display(Name = "Quantity Recieved")]
        public int QuantityRecieved {  get; set; }

        [Display(Name = "Quantity Defective")]
        public int QuantityDefect {  get; set; }

        [Display(Name = "Description of Defect")]
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
