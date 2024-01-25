namespace Haver_Niagara.Models
{
    public class Engineering
    {
        public int ID { get; set; }
        public bool CustomerNotify { get; set; }
        public bool DrawUpdate { get; set; }
        public string Disposition { get; set; }
        public int RevisionOriginal { get; set; }
        public int RevisionUpdated { get; set; }
        public DateTime RevisionDate { get; set; }
        public string EngSignature { get; set; }
        public DateTime EngSignatureDate { get; set; }
        public EngineeringDecision EngDecison { get; set; }
        public NCR NCR { get; set; }


    }
}
