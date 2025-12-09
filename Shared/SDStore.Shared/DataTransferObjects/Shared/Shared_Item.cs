namespace SDStore.Shared.DataTransferObjects.Shared
{
    using SDStore.Data.Shared.Enums;
    using SDStore.Data.Shared.Structs;
    
    using System.ComponentModel.DataAnnotations;
    
    public abstract record Shared_Item
    {
        [Required(ErrorMessage = "Product name is a required field.")]
        [StringLength(150, MinimumLength = 5,
            ErrorMessage = "Product name must be between 5 and 150 characters long.")]
        public required string Name { get; init; }

        public Money Price { get; init; }
        
        [Required]
        [EnumDataType(typeof(PriceMode),
            ErrorMessage = "PriceMode must be one of the following: PerItem, PerKg, PerLiter")]
        public PriceMode PriceMode { get; init; }
    }
}