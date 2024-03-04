using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class NewNCR
    {
        public int Id { get; set; }
        public int NewNCRNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //One to One with Quality Inspection Table
        public int? QualityInspectionID { get; set; }
        public QualityInspection QualityInspection { get; set; }


        //navigation property
        public int NCRId { get; set; }
        public NCR NCR { get; set; }

        //
    }
}
