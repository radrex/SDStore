namespace SDStore.Data.Shared.Enums
{
    using System.ComponentModel.DataAnnotations;
    
    public enum PriceMode
    {
        [Display(Name = "Unit: item")]
        PerItem,
        
        [Display(Name = "Unit: kg.")]
        PerKg,
        
        [Display(Name = "Unit: l.")]
        PerLiter,
    }
}