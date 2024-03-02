using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class Engineering
    {
        public int ID { get; set; }
        [Display(Name = "Notify Customer Required?")]
        public bool CustomerNotify { get; set; }
        [Display(Name = "Drawing Updated?")]
        public bool DrawUpdate { get; set; }
        [Display(Name = "Engineering Disposition")]
        public string Disposition { get; set; }
        [Display(Name = "Original Revision Number")]
        public int RevisionOriginal { get; set; }
        [Display(Name = "Updated Revision Number")]
        public int RevisionUpdated { get; set; }
        [Display(Name = "Revision Date")]
        public DateTime RevisionDate { get; set; }
        [Display(Name = "Engineering Name")]

       

        public string EngSignature { get; set; }
        [Display(Name = "Engineering Date")]
        public DateTime EngSignatureDate { get; set; }
        [Display(Name = "Engineering Decision")]
        public EngineeringDecision EngDecision { get; set; }
        public NCR NCR { get; set; }
    }
}
