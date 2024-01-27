using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class DefectList
    {
        [Key]
        public int DefectListID { get; set; }
        public string Name { get; set; }
        public ICollection<Defect> Defects { get; set; } = new HashSet<Defect>();

    }
}
