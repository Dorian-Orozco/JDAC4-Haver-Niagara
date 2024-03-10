using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class Engineering : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter an Engineer's Name")]
        [Display(Name = "Engineering Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Notify Customer Required?")]
        public bool CustomerNotify { get; set; }

        [Required(ErrorMessage = "Select if Drawing Requires Updating")]
        [Display(Name = "Drawing Updated?")]
        public bool DrawUpdate { get; set; }


        
        [Required(ErrorMessage = "Engineer's Disposition Notes Required")]
        [Display(Name = "Disposition Sequence")]
        public string DispositionNotes { get; set; }


        [Display(Name = "Original Revision Number")]
        public int RevisionOriginal { get; set; }


        [Display(Name = "Updated Revision Number")]
        public int RevisionUpdated { get; set; }


        [Display(Name = "Revision Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RevisionDate { get; set; }


        [Display(Name = "Engineering Disposition")]
        [Required(ErrorMessage = "Please Select a Decision")]
        public EngineeringDisposition EngineeringDisposition { get; set; }
        //Link to NCR 
        public NCR NCR { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var TodaysDate = DateTime.Today;
            if (Date > TodaysDate || RevisionDate > TodaysDate)
            {
                yield return new ValidationResult("Date Cannot be in The Future", new[] { "Date", "RevisionDate"});
            }
        }
    }
}
