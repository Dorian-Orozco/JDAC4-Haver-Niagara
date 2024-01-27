﻿namespace Haver_Niagara.Models
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


        //1 to many relationship with product
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public Purchasing Purchasing { get; set; }


        //1:1 relationship with ncr
        public NewNCR? NewNCR { get; set; }

        //Reference to DEPENDANT entity (engineering) 
        public Engineering Engineering { get; set; }

    }
}
