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
        }
    }
}