using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class Engineering
    {
        //Review this model, not sure if done correctly 
        public int ID { get; set; }

        [Display(Name="Engineering Name")]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }  

        [Display(Name = "Notify Customer Required?")]
        public bool CustomerNotify { get; set; }


        [Display(Name = "Drawing Updated?")]
        public bool DrawUpdate { get; set; }


        // "eng_notes" in data model...unsure of.
        [Display(Name = "Disposition Sequence")]
        public string DispositionNotes { get; set; }


        [Display(Name = "Original Revision Number")]
        public int RevisionOriginal { get; set; }
        

        [Display(Name = "Updated Revision Number")]
        public int RevisionUpdated { get; set; }
        

        [Display(Name = "Revision Date")]
        public DateTime RevisionDate { get; set; }
        
       
        [Display(Name = "Revision Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RevDate { get; set; }


        [Display(Name = "Engineering Disposition")]
        public EngineeringDisposition EngineeringDisposition { get; set; }
        //Link to NCR 
        public NCR NCR { get; set; }
    }
}
