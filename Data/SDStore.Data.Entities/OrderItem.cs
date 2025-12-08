namespace SDStore.Data.Entities
{
    using Shared.Enums;
    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class OrderItem
    {
        #region FOREIGN KEY PROPERTIES [FK]
        
        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }
        
        public int ItemId { get; set; }
        public virtual Item? Item { get; set; }

        #endregion

        #region PROPERTIES
        
        /// <summary>
        ///     <list type="bullet">
        ///         <listheader>
        ///             <term>Based on <see cref="Item.PriceMode"/>. Examples:</term>
        ///         </listheader>
        ///         <item>
        ///             <term>PerItem</term>
        ///             <description> - 3 items</description>
        ///         </item>
        ///         <item>
        ///             <term>PerKg</term>
        ///             <description> - 0.82kg</description>
        ///         </item>
        ///         <item>
        ///             <term>PerLiters</term>
        ///             <description> - 10.50l</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        public Currency Currency { get; set; }
        
        /// <summary>
        /// <c>Subtotal_Gross = Item.Price * OrderItem.Amount</c>, which is universal for all Modes.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal_Gross { get; set; }

        /// <summary>
        /// https://worldpopulationreview.com/country-rankings/vat-tax-by-country
        /// Hungary... you are way too hungry...
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(4,2)")]
        [Range(typeof(decimal),"0.00", "27.00",
            ErrorMessage = "VAT Percentage must be between 0.00 and 27.00.")]
        public decimal VAT_Percentage { get; set; }

        /// <summary>
        /// Initially I was thinking to make it as a Calculated Property in memory, but then I decided to move them to the db
        /// <c>VAT_Excluded = BankersRounding(((SubTotal_Gross / (1 + (VAT_Percentage / 100))) - SubTotal_Gross) * -1)</c>
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal VAT_Excluded { get; set; }
        
        /// <summary>
        /// Initially I was thinking to make it as a Calculated Property in memory, but then I decided to move them to the db
        /// <c>SubTotal_Net = SubTotal_Gross - VAT_Excluded</c>
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal_Net { get; set; }
        
        #endregion
    }
}