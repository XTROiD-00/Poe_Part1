using Microsoft.EntityFrameworkCore;
using Poe_Part1.Models;

namespace Poe_Part1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>()
                .Property(c => c.Rate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Claim>()
                .Property(c => c.TotalAmount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
