using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class NCR
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "NCR No.")]
        [Required(ErrorMessage = "You cannot leave the NCR number blank.")] 
        public string NCR_Number { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NCR_Date{ get; set; }


        [Display(Name ="Status")]
        public bool NCR_Status { get; set; }

        //NCR Enumeration To Determine the Stage
        [Display(Name = "Stage")]
        public NCRStage NCR_Stage { get; set; }

        // PART ENTITY //
        [ForeignKey("Part")]
        public int? PartID { get; set; }
        public Part Part{ get; set; }


        // PURCHASING //
        [ForeignKey("Operation")]
        public int? OperationID { get; set; }   
        public Operation Operation { get; set; }

        // ENGINEERING // 
        [ForeignKey("Engineering")]
        public int? EngineeringID { get; set; }
        public Engineering Engineering { get; set; }


        // NEW NCR // Relationship must be one to one
        public NewNCR? NewNCR { get; set; }
    
        ////NCR can have many Employees? 
        //public ICollection<Employee> Employees { get; set; }
    }
}
