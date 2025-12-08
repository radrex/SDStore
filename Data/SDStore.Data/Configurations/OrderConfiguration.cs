namespace SDStore.Data.Configurations
{
    using Entities;
    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order.Property(i => i.Currency)
                 .HasConversion<string>();
        }
    }
}