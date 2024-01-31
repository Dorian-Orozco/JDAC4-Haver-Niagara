using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class NCR
    {
        public int ID { get; set; }

        [Display(Name = "NCR No.")]
        [Required(ErrorMessage = "You cannot leave the NCR number blank.")] 
        public int NCR_Number {  get; set; }

        [Display(Name = "Sales Order")]
        public string SalesOrder {  get; set; }

        [Display(Name = "Inspector Name")]
        public string InspectName { get; set; }

        [Display(Name = "Inspected Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InspectDate { get; set; }

        [Display(Name = "Close NCR?")]
        public bool NCRClosed { get; set; }

        [Display(Name = "Quality Representative")]
        public string QualSignature {  get; set; }

        [Display(Name = "Quality Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime QualDate { get; set; }


        // PRODUCT //
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Display(Name = "PO or Prod. Number")]
        public Product Product { get; set; }

        // PURCHASING //
        [ForeignKey("Purchasing")]
        public int PurchasingID { get; set; }   
        public Purchasing Purchasing { get; set; }

        // ENGINEERING //
        [ForeignKey("Engineering")]
        public int EngineeringID { get; set; }
        public Engineering Engineering { get; set; }


        // NEW NCR //
        public NewNCR? NewNCR { get; set; }
    }
}
