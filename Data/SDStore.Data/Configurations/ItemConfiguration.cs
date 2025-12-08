namespace SDStore.Data.Configurations
{
    using Entities;
    using Shared.Structs;
    
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> item)
        {
            item.Property(i => i.PriceMode)
                .HasConversion<string>();

            item.Property(i => i.Price)
                .HasConversion(
                    m => JsonSerializer.Serialize(m, new JsonSerializerOptions
                    {
                        Converters = { new JsonStringEnumConverter() }
                    }),
                    m => JsonSerializer.Deserialize<Money>(m, new JsonSerializerOptions
                    {
                        Converters = { new JsonStringEnumConverter() }
                    }));
        }
    }
}