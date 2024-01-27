namespace Haver_Niagara.Models
{
    public class NewNCR
    {
        public int Id { get; set; }
        public int NewNCRNumber { get; set; }

        //Framework can determine 1:1 with ID
        public int NCRId { get; set; }
        public NCR NCR { get; set; }
    }
}
