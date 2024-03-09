using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public enum NCRStage
    {
        [Display(Name = "1. Quality Rep")]
        QualityRepresentative,
        [Display(Name = "Engineering")]
        Engineering,
        [Display(Name = "Purchasing")]
        Purchasing,
        [Display(Name = "Quality Rep")]
        QualityRepresentative_Final,
        [Display(Name = "Complete")]
        Completed,
        [Display(Name = "Closed")] //Added Closed Option        
        Closed_NCR
    }
}
