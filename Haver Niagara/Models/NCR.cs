using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Close NCR")]
        public bool NCRClosed { get; set; }

        [Display(Name = "Quality Representative")]
        public string QualSignature {  get; set; }

        [Display(Name = "Quality Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime QualDate { get; set; }


        //1 to many relationship with product
        public int ProductID { get; set; }

        [Display(Name = "PO or Prod. Number")]
        public Product Product { get; set; }

        public Purchasing Purchasing { get; set; }


        //1:1 relationship with ncr
        public NewNCR? NewNCR { get; set; }

        //Reference to DEPENDANT entity (engineering) 
        public Engineering Engineering { get; set; }

    }
}

// Data Annotations
// [StringLength(50)]
// [StringLength(50, ErrorMessage = "Cannot be more than 50 characters long.")]
// [RegularExpression("^\\d{10}$", ErrorMessage = "Number must be exactly 10 digits.")]
// [Range(1, 12, ErrorMessage = "The number of expected visits must be between 1 and 12.")]
// [DataType(DataType. )]