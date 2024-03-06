using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name ="Full Name")]
        public string FullName => FirstName + " " + LastName;

        public string Role { get; set; }

        public string Email { get; set; }

        //One to One with NCR? 
        //public int NCRId { get; set; }
        //public NCR NCR { get; set; }
    }
}
