using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class NCR
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name ="NCR No.")]
        public string FormattedID
        {
            get
            {
                return $"{DateTime.UtcNow.Year}-{ID.ToString().PadLeft(3, '0')}";
            }
        }

        //March 05 : Using ID for NCR Number instead of having an NCR Number. ID is auto generated and cannot be changed once db assigns.
        //[Display(Name = "NCR No.")]
        //[Required(ErrorMessage = "You cannot leave the NCR number blank.")] 
        //public string NCR_Number { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NCR_Date { get; set; }


        [Display(Name = "Status")]
        public bool NCR_Status { get; set; }


        public NCR()
        {
            //Sets the NCR_Status to false as default, because when an NCR is first created it cannot be true (finished).
            NCR_Status = false;
            //Defaulting when creating an NCR to have the quality representative as the first stage
            NCR_Stage = NCRStage.QualityRepresentative;
            //Setting a default of todays date
            NCR_Date = DateTime.Today;
        }

        //NCR Enumeration To Determine the Stage
        [Display(Name = "Stage")]
        public NCRStage NCR_Stage { get; set; }



        // PART ENTITY //
        [ForeignKey("Part")]
        public int? PartID { get; set; }
        public Part Part { get; set; }


        // PURCHASING //
        [ForeignKey("Operation")]
        public int? OperationID { get; set; }
        public Operation Operation { get; set; }

        // ENGINEERING // 
        [ForeignKey("Engineering")]
        public int? EngineeringID { get; set; }
        public Engineering Engineering { get; set; }

        [ForeignKey("QualityInspection")]
        public int? QualityInspectionID { get; set; }
        public QualityInspection QualityInspection { get; set; }

        // NEW NCR // Relationship must be one to one
        [ForeignKey("NewNCRID")]
        public int? NewNCRID { get; set; }
        public NewNCR NewNCR { get; set; }

        ////NCR can have many Employees? 
        //public ICollection<Employee> Employees { get; set; }
    }
}
