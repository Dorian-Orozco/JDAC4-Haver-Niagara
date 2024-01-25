namespace Haver_Niagara.Models
{
    public class Purchasing
    {
        public int ID {  get; set; }
        public string PurchaseSignature { get; set; }
        public DateTime SignatureDate { get; set; }
        public PurchasingDecision PurchasingDec { get; set; }
        public FollowUp? followUp { get; set; }
        public CAR? CAR { get; set; }

    }
}
