using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public enum NCRStage
    {
        [Display(Name = "Quality Rep")] //0
        QualityRepresentative,
        [Display(Name = "Engineering")] //1
        Engineering,
        [Display(Name = "Purchasing")] //2
        Purchasing,
        [Display(Name = "Procurement")] //3
        Procurement,
        [Display(Name = "Quality Rep")] //4
        QualityRepresentative_Final,
        //[Display(Name = "Complete")]
        //Completed,
        [Display(Name = "Complete")] //5       
        Closed_NCR
    }
}
