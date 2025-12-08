namespace SDStore.Data
{
    using Entities;
    using Extensions;
 
    using Microsoft.EntityFrameworkCore;
    
    public sealed class SDStoreDBContext(DbContextOptions<SDStoreDBContext> options) : DbContext(options)
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyGlobalRestrictDeleteRule(); // No CASCADE DELETE for safety. 
            modelBuilder.SeedDatabase();
        }
    }
}