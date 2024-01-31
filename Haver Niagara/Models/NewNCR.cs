namespace Haver_Niagara.Models
{
    public class NewNCR
    {
        public int Id { get; set; }
        public int NewNCRNumber { get; set; }

        public int NCRId { get; set; }
        public NCR NCR { get; set; }
    }
}
