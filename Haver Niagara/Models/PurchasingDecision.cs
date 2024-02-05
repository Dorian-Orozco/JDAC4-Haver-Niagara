using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    public enum PurchasingDecision
    {
        [Display(Name ="Rework In House")]
        ReworkInHouse,
        [Display(Name ="Scrap In House")]
        ScrapInHouse,
        [Display(Name ="Defer To Engineering")]
        DeferToEngineering,
        [Display(Name ="Return To Supplier")]
        ReturnToSupplier
    }
}
