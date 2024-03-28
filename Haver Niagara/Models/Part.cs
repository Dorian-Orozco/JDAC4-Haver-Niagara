﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Haver_Niagara.Models
{
    public class Part : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Part Name")]
        [Required(ErrorMessage = "Please Enter Part Name")]
        public string Name { get; set; }

        [Display(Name = "Part Number")]
        public int PartNumber { get; set; }

        [Display(Name = "SAP")] //SAME AS PART NUMBER?
        [Required(ErrorMessage = "Please Enter Sap Number")]
        public int SAPNumber { get; set; }

        [Display(Name = "PO")]
        public long PurchaseNumber { get; set; }

        [Display(Name = "Sales #")]
       
        public string SalesOrder { get; set; }

        [Display(Name = "Prod #")]
        public int ProductNumber { get; set; }

        [Required]
        [Display(Name = "Quantity Received")]
        public int QuantityRecieved { get; set; }

        [Required]
        [Display(Name = "Quantity Defective")]
        public int QuantityDefect { get; set; }


        [Display(Name = "Description of Item")]
        [Required(ErrorMessage = "Please Enter Item Description")]
        public string Description { get; set; }

        //navigation property for supplier
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        //One to many with NCR
        public NCR NCR { get; set; }
        //public ICollection<NCR> NCRs { get; set; } = new HashSet<NCR>();

        public ICollection<DefectList> DefectLists { get; set; } = new HashSet<DefectList>();
        public ICollection<Media> Medias { get; set; } = new HashSet<Media>();

        //Validates to make sure quantity recieved CANNOT be greater than the amount defective
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(QuantityDefect > QuantityRecieved)
            {
                yield return new ValidationResult("Quantity Defective Cannot Exceed Quantity Receieved", new[] { "QuantityDefect" });
            }
            if(QuantityDefect <= 0 || QuantityRecieved <= 0)
            {
                yield return new ValidationResult("Please Enter a Postive Number", new[] { "QuantityDefect","QuantityRecieved" });
            }
        }
    }
}
