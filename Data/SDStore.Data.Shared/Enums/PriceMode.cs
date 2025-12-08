namespace SDStore.Data.Shared.Enums
{
    using System.ComponentModel.DataAnnotations;
    
    public enum PriceMode
    {
        [Display(Name = "pc.")]
        PerItem,
        
        [Display(Name = "kg.")]
        PerKg,
        
        [Display(Name = "l.")]
        PerLiter,
    }
}