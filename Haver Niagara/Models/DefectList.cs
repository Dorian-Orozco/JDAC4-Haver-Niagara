using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class DefectList
    {
        [Key]
        public int DefectListID { get; set; }
        
        //Setting up junction table remove code if doesnt work

        //Foreign key to defect
        public int DefectID { get; set; }   
        public Defect Defect { get; set; }


        //foreign key to product
        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
