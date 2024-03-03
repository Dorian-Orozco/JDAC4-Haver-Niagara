using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class QualityInspection
    {
        //Need to add data annotations? 

        public int ID { get; set; } 

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name="Item Marked")]
        public bool ItemMarked { get; set; }

        [Display(Name="Re-Inspected?")]
        public bool ReInspected { get; set; }

        //One to One with New NCR
        public int NewNCRID { get; set; }
        public NewNCR NewNCR { get; set; }

        //Navigation property for NCR
        public int NCRId { get; set; }
        public NCR NCR { get; set; }
    }
}
