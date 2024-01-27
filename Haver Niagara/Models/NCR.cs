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
        public Engineering Engineering { get; set; }
        public Product Product { get; set; }
        public Purchasing Purchasing { get; set; }
        public NewNCR? NewNCR { get; set; }






        



    }
}
