using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class QualityInspection : IValidatableObject
    {
        //Auto generating ID /Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage ="Representative Name Required")]
        [Display(Name = "Quality Representative's Name")]
        public string Name { get; set; }

        [Display(Name = "Quality Representative Inspection Date")]
        [Required(ErrorMessage = "Date Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //Added properties to better track Quality Rep
        //[Required(ErrorMessage ="Inspector's Name Required")]
        [Display(Name ="Inspector's Name")] 
        public string InspectorName { get; set; }
        
        [Display(Name = "Inspected Date")]
        //[Required(ErrorMessage ="Inspector Date Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InspectorDate { get; set; }

        [Display(Name = "Quality Department")]
        public string Department { get; set; }

        [Display(Name = "Department Date")]
        //[Required(ErrorMessage="Department Date Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DepartmentDate { get; set; }

        [Required(ErrorMessage = "Please indicate if Item is Marked")]
        [Display(Name = "Item Marked Non-Conforming")]
        public bool ItemMarked { get; set; }

        [Display(Name = "Re-Inspected?")]
        public bool ReInspected { get; set; }

        [Display(Name = "Quality Identify")]
        [Required(ErrorMessage = "Please Select The Process Applicable")]
        public QualityIdentify QualityIdentify { get; set; }

        //Navigation property for NCR
        public NCR NCR { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var TodaysDate = DateTime.Today;
            if(Date > TodaysDate || InspectorDate > TodaysDate || DepartmentDate > TodaysDate) 
            {
                yield return new ValidationResult("Date Cannot be in The Future", new[] { "Date", "InspectorDate", "DepartmentDate" });
            }
        }
    }
}
