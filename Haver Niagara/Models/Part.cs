using System.ComponentModel.DataAnnotations;


namespace Haver_Niagara.Models
{
    public class Part
    {
        public int ID { get; set; }

        [Display(Name = "Part Name")]
        public string Name { get; set; }

        [Display(Name = "Part Number")]
        public int PartNumber { get; set; }

        [Display(Name="SAP")]
        public int SAPNumber { get; set; }

        [Display(Name="PO")]
        public int PurchaseNumber { get; set; }

        [Display(Name="Sales #")]
        public int SalesOrder { get; set; }

        [Display(Name="Prod #")]
        public int ProductNumber { get; set; }

        [Display(Name = "Quantity Recieved")]
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
