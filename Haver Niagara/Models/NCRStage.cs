using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public enum NCRStage
    {
        [Display(Name = "1. Quality Representative")]
        QualityRepresentative,
        [Display(Name = "2. Engineering")]
        Engineering,
        [Display(Name = "3. Purchasing")]
        Purchasing,
        [Display(Name = "4. Quality Representative")]
        QualityRepresentative_Final
    }
}
