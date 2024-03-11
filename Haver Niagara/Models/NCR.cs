﻿using System.ComponentModel.DataAnnotations;
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
                return $"{NCR_Date.Year}-{ID.ToString().PadLeft(3, '0')}";
            }
        }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NCR_Date { get; set; }

        [Required(ErrorMessage = "Keep NCR Open? field is required")]
        [Display(Name = "Status")]
        public bool NCR_Status { get; set; } //If NCR status == true then report is active, else, report is closed!

        //new properties for new ncrs

        [Display(Name = "New NCR Number")]
        public int? NewNCRID { get; set; }

        [Display(Name ="Old NCR Number")]
        public int? OldNCRID { get; set; }

        public bool? IsArchived
        {
            get
            {
                int daysInFiveYears = 365 * 5 + 1; // 365 days per year + 1 additional day for possible leap year

                if (DateTime.Now.Subtract(NCR_Date).Days >= daysInFiveYears)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            set { }
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

        // QUALITY //
        [ForeignKey("QualityInspection")]
        public int? QualityInspectionID { get; set; }
        public QualityInspection QualityInspection { get; set; }

        // PROCUREMENT //
        [ForeignKey("Procurement")]
        public int? ProcurementID { get; set; }
        public Procurement Procurement { get; set; }

        // DEFAULT CONSTRUCTOR //
        public NCR()
        {
            //Sets the NCR_Status to true because its active bc it was just made
            NCR_Status = true; //change back to false if everthing breaks
            //Defaulting when creating an NCR to have the quality representative as the first stage
            NCR_Stage = NCRStage.Engineering;
            //Setting a default of todays date
            NCR_Date = DateTime.Today;
        }
    }
}
