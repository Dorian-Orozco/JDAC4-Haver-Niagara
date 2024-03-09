﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haver_Niagara.Models
{
    public class Procurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } //PK

        [Display(Name = "Return rejected items to supplier")]
        public bool ReturnRejected { get; set; } //Return rejected items to supplier?

        //if ReturnRejected = true (yes)
        [Display(Name = "RMA No.")]
        public int RMANumber {  get; set; }

        [Display(Name = "Carrier")]
        public string CarrierName {  get; set; }

        [Display(Name = "Carrier Phone No.")]
        public string CarrierPhone { get; set; }

        [Display(Name = "Account No.")]
        public int AccountNumber { get; set; }

        [Display(Name = "Dispose on site")] 
        public bool DisposeOnSite {  get; set; } //if ReturnRejected = false (no)

        [Display(Name = "When will replacement/reworked item be received/returned?")]
        public DateTime ToReceiveDate { get; set; } //When will replacement/reworked item be received/returned?

        [Display(Name = "Supplier return has been completed in SAP")]
        public bool SuppReturnCompletedSAP {  get; set; } //Supplier return has been completed in SAP

        [Display(Name = "Expecting credit from supplier")]
        public bool ExpectSuppCredit {  get; set; } //Expecting credit from supplier

        [Display(Name = "Billing supplier for expenses incurred in the rework process")]
        public bool BillSupplier {  get; set; } //Billing supplier for expenses incurred in rework process

        public NCR NCR { get; set; } //FK

    }
}