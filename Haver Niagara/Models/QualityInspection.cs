using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class QualityInspection
    {
        //Auto generating ID /Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Quality Representative's Name")]
        public string Name { get; set; }

        [Display(Name = "Quality Inspection Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Item Marked Non-Conforming")]
        public bool ItemMarked { get; set; }

        [Display(Name = "Re-Inspected?")]
        public bool ReInspected { get; set; }

        [Display(Name = "Quality Identify")]
        public QualityIdentify QualityIdentify { get; set; }

        //One to One with New NCR
        public NewNCR NewNCR { get; set; }

        //Navigation property for NCR
        public NCR NCR { get; set; }
    }
}
