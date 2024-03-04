using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class Supplier
    {
        public int ID { get; set; }

        [Display(Name = "Supplier Name")]
        public string Name { get; set; }
        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }   
}
