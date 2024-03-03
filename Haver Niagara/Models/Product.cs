using System.ComponentModel.DataAnnotations;


namespace Haver_Niagara.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Product Number")]
        public int ProductNumber { get; set; }

        [Display(Name = "Quantity Received")]
        public int QuantityRecieved {  get; set; }

        [Display(Name = "Quantity Defective")]
        public int QuantityDefect {  get; set; }

        [Display(Name = "Description of Item")]
        public string Description { get; set; }

        //navigation property for supplier 
        public Supplier Supplier { get; set; }

        //One to many with NCR
        public NCR NCR { get; set; }
        //public ICollection<NCR> NCRs { get; set; } = new HashSet<NCR>();

        public ICollection<DefectList> DefectLists { get; set; } = new HashSet<DefectList>();
        public ICollection<Media> Medias { get; set; } = new HashSet<Media>(); 

    }
}
