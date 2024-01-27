namespace Haver_Niagara.Models
{
    public class NCR
    {
        public int ID { get; set; }
        public int NCR_Number {  get; set; }
        public string SalesOrder {  get; set; }
        public string InspectName { get; set; }
        public DateTime InspectDate { get; set; }
        public bool NCRClosed { get; set; }
        public string QualSignature {  get; set; }
        public DateTime QualDate { get; set; }
        public Product Product { get; set; }
        public Purchasing Purchasing { get; set; }

        //Adding NewNCR ID to let framework determine whos dependant in 1:1
        public int NewNCRId { get; set; } //
        public NewNCR? NewNCR { get; set; }

        //Adding EngineeringID as potential foreign key (itll determine which side is dependant for 1:1)
        public int EngineerId { get; set; } //
        public Engineering Engineering { get; set; }









    }
}
