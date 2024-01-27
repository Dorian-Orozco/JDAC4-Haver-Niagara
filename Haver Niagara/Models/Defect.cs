using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class Defect
    {
        public int ID { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public ICollection<DefectList> DefectLists { get; set; } = new HashSet<DefectList>();
    }
}
