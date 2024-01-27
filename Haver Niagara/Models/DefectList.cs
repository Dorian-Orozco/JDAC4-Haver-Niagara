using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class DefectList
    {   
        public string Name { get; set; }
        public ICollection<Defect> Defects { get; set; } = new HashSet<Defect>();

    }
}
