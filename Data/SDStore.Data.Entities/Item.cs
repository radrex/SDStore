namespace SDStore.Data.Entities
{
    using Shared.Enums;
    using Shared.Structs;
    
    using System.ComponentModel.DataAnnotations;
    
    public class Item
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(150, MinimumLength = 5,
            ErrorMessage = "Product name must be between 5 and 150 characters long.")]
        public required string Name { get; set; }
        
        public Money Price { get; set; }
        
        [Required]
        [EnumDataType(typeof(PriceMode),
            ErrorMessage = "PriceMode must be one of the following: PerItem, PerKg, PerLiter")]
        public PriceMode PriceMode { get; set; }
        
        #region INVERSE FOREIGN KEY PROPERTIES [FK]
        
        public virtual ICollection<OrderItem> Orders { get; set; } = new HashSet<OrderItem>();

        #endregion
    }
}