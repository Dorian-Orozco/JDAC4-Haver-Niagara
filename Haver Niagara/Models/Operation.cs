using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class Operation
    {
        public int ID {  get; set; }
        
        [Display(Name = "Operation Name")]
        public string Name { get; set; }


        [Display(Name = "Operation Date")]
        public DateTime OperationDate{ get; set; }
        
        [Display(Name = "Operation Decision")]
        public OperationDecision OperationDecision { get; set; }

        [Display(Name="Operation Notes")]
        public string OperationNotes { get; set; }

        //For Radio Buttons T/F 
        [Display(Name="Car Raised?")]
        public bool OperationCar { get; set; }

        //For Radio Buttons T/F 
        [Display(Name="Follow-Up Required?")]
        public bool OperationFollowUp { get; set; }
        
        
        //Follow Up Property
        public FollowUp FollowUp { get; set; }

        //Car Property
        public int CarID { get; set; }
        public CAR CAR { get; set; }


        //NCR Property
        public NCR NCR { get; set; }


    }
}
