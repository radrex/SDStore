namespace SDStore.Data.Configurations
{
    using Entities;
    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> orderItem)
        {
            // COMPOSITE PRIMARY KEY
            orderItem.HasKey(oi => new { oi.ItemId, oi.OrderId });

            #region FOREIGN KEYS

            orderItem
                .HasOne(oi => oi.Item)
                .WithMany(i => i.Orders)
                .HasForeignKey(oi => oi.ItemId);

            orderItem
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            #endregion
            
            orderItem.Property(i => i.Currency)
                     .HasConversion<string>();
            
            orderItem.ToTable(tb =>
            {
                tb.HasCheckConstraint(
                    "CK_OrderItem_Currency_AllowedCurrencies",
                    "[Currency] IN ('BGN', 'EUR', 'USD', 'CAD', 'GBP')");

                // TODO: Add check constraint for Amount
                // [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must have at most 2 decimal places.")]
                // [Range(0, 9999999999999999.99, ErrorMessage = "Amount should be in 0 - 9999999999999999.99 range.")]
                
                tb.HasCheckConstraint(
                    "CK_OrderItem_VAT_Percentage_AllowedPercentages",
                    "[VAT_Percentage] >= 0.00 AND [VAT_Percentage] <= 27.00"
                );

            });
        }
    }
}