using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public enum NCRStage
    {
        [Display(Name = "1. Quality Rep")]
        QualityRepresentative,
        [Display(Name = "2. Engineering")]
        Engineering,
        [Display(Name = "3. Purchasing")]
        Purchasing,
        [Display(Name = "4. Quality Rep")]
        QualityRepresentative_Final,
        [Display(Name = "Complete")]
        Completed,
        [Display(Name = "Closed")] //Added Closed Option        
        Closed_NCR
    }
}
