using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class Purchasing
    {
        public int ID {  get; set; }
        [Display(Name = "Purchasing Name")]
        public string PurchaseSignature { get; set; }
        [Display(Name = "Purchasing Date")]
        public DateTime SignatureDate { get; set; }
        [Display(Name = "Purchasing Decision")]
        public PurchasingDecision PurchasingDec { get; set; }
        [Display(Name = "Follow Up")]
        public FollowUp? followUp { get; set; }
        [Display(Name = "CAR Raised")]
        public CAR? CAR { get; set; }

        public NCR NCR { get; set; }


    }
}
