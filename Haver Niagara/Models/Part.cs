using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Haver_Niagara.Models
{
    public class Part
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Part Name")]
        public string Name { get; set; }

        [Display(Name = "Part Number")]
        public int PartNumber { get; set; }

        [Display(Name="SAP")]
        public int SAPNumber { get; set; }

        [Display(Name="PO")]
        public long PurchaseNumber { get; set; }

        [Display(Name="Sales #")]
        public string SalesOrder { get; set; }

        [Display(Name="Prod #")]
        public int ProductNumber { get; set; }

        [Display(Name = "Quantity Received")]
        public int QuantityRecieved {  get; set; }

        [Display(Name = "Quantity Defective")]
        public int QuantityDefect {  get; set; }

        [Display(Name = "Description of Item")]
        public string Description { get; set; }

        //navigation property for supplier
        [Display(Name="Supplier")]
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        //One to many with NCR
        public NCR NCR { get; set; }
        //public ICollection<NCR> NCRs { get; set; } = new HashSet<NCR>();

        public ICollection<DefectList> DefectLists { get; set; } = new HashSet<DefectList>();
        public ICollection<Media> Medias { get; set; } = new HashSet<Media>(); 

    }
}
