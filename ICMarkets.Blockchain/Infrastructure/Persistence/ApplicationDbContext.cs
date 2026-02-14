
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BlockchainInfo> Blockchains => Set<BlockchainInfo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Requirement 3: Sorting by CreatedAt descending is easier with an index
            modelBuilder.Entity<BlockchainInfo>().HasIndex(b => b.CreatedAt);
        }
    }
}
