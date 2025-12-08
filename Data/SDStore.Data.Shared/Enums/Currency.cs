namespace SDStore.Data.Shared.Enums
{
    using System.ComponentModel.DataAnnotations;
    
    public enum Currency
    {
        [Display(Name = "lv.")]
        BGN,

        [Display(Name = "€")]
        EUR,
        
        [Display(Name = "$")]
        USD,
        
        [Display(Name = "C$")]
        CAD,
        
        [Display(Name = "£")]
        GBP,
    }
}