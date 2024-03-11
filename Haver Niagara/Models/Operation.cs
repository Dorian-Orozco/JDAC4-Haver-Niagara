using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class Operation : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Operation Name")]
        [Required(ErrorMessage ="Operation's Name Required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Date is Required")]
        [Display(Name = "Operation Date")]
        public DateTime OperationDate { get; set; }

        [Required(ErrorMessage="Prelimnary Decision Required")]
        [Display(Name = "Operation Decision")]
        public OperationDecision OperationDecision { get; set; }

        [Display(Name = "Operation Notes")]
        public string OperationNotes { get; set; }

        //For Radio Buttons T/F 
        [Required]
        [Display(Name = "Car Raised?")]
        public bool OperationCar { get; set; }

        //For Radio Buttons T/F 
        [Required]
        [Display(Name = "Follow-Up Required?")]
        public bool OperationFollowUp { get; set; }


        //Follow Up Property
        public FollowUp FollowUp { get; set; }

        //Car Property
        public CAR CAR { get; set; }


        //NCR Property
        public NCR NCR { get; set; }

        public Operation()
        {
            OperationDate = DateTime.Today;
            
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var TodaysDate = DateTime.Today;
            if (OperationDate > TodaysDate)
            {
                yield return new ValidationResult("Date Cannot be in The Future", new[] { "OperationDate"});
            }
            if(OperationCar && CAR != null) //if true then look for values if none throw err
            {
                if (CAR.CARNumber == null)
                    yield return new ValidationResult("Car Number Cannot Be Empty", new[] { "CAR.CARNumber" });
                if(CAR.CARNumber <= 0)
                    yield return new ValidationResult("Car Number Cannot Be Negative", new[] { "CAR.CARNumber" });
                if(CAR.Date == default(DateTime))
                    yield return new ValidationResult("Car Date Cannot Be Empty", new[] { "CAR.Date" });
                if(CAR.Date > TodaysDate)
                    yield return new ValidationResult("Car Date Cannot Be In The Future", new[] { "CAR.Date" });
            }
            if (!OperationCar && CAR != null)
            {
                CAR.CARNumber = null;
                CAR.Date = default;
            }
            if (OperationFollowUp && FollowUp != null)
            {
                if(string.IsNullOrEmpty(FollowUp.FollowUpType))
                    yield return new ValidationResult("Follow Up Type Cannot Be Empty", new[] { "FollowUp.FollowUpType" });
                if(FollowUp.FollowUpDate == default(DateTime))
                    yield return new ValidationResult("Follow Up Date Cannot Be Empty", new[] { "FollowUp.FollowUpDate" });
                if(FollowUp.FollowUpDate > TodaysDate)
                    yield return new ValidationResult("Follow Up Date Cannot Be In The Future", new[] { "FollowUp.FollowUpDate" });
            }
            if (!OperationFollowUp &&FollowUp != null)
            {
                FollowUp.FollowUpType = string.Empty;
                FollowUp.FollowUpDate = default;
            }
        }
    }
}
