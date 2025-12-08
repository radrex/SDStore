namespace SDStore.Data.Extensions
{
    using Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    
    // TODO: convert to new syntax from .net10/c#14 extension grouping
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Globally sets the Foreign Keys <see cref="DeleteBehavior"/> to <see cref="DeleteBehavior.Restrict"/> to prevent accidental Cascade deletions of entities in the Database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ApplyGlobalRestrictDeleteRule(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableForeignKey foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            DatabaseSeeder.Seed(modelBuilder);
        }
    }
}