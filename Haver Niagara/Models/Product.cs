using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Haver_Niagara.Models
{
    public class Product
    {
        public int ID { get; set; } 
        public string Name { get; set; }    
        public int QuantityRecieved {  get; set; }
        public int QuantityDefect {  get; set; }
        public string Description { get; set; }

        public ICollection<DefectList> DefectLists { get; set; } = new HashSet<DefectList>();
        public ICollection<Media> Medias { get; set; } = new HashSet<Media>();



    }
}
