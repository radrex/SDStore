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

            order.ToTable(tb =>
            {
                tb.HasCheckConstraint(
                    "CK_Order_Currency_AllowedCurrencies",
                    "[Currency] IN ('BGN', 'EUR', 'USD', 'CAD', 'GBP')");
                
                // TODO: For PhoneNumber - [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
                // TODO: For Email
            });
        }
    }
}
