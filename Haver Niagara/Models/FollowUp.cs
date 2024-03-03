using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class FollowUp
    {

        public int ID { get; set; }

        [Display(Name ="Follow Up Date")]
        public DateTime FollowUpDate { get; set; }

        [Display(Name ="Follow Up Type")]
        public string FollowUpType {  get; set; }


        [ForeignKey("Operation")]
        public int? OperationID { get; set; }
        public Operation Operation { get; set; }
    }
}
