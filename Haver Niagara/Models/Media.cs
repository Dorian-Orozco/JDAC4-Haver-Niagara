using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public class Media
    {
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Content { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        public string MimeType { get; set; }

        public string Links { get; set; }
        //foreign key, 
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
