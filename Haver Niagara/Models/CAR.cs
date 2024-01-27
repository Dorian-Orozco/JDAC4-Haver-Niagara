using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Haver_Niagara.Models
{
    public class CAR
    {
        [Key]
        public int ID { get; set; } 
        public int CARNumber { get; set; }
    }
}
