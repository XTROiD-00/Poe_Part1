using Microsoft.EntityFrameworkCore;
using Poe_Part1.Models;

namespace Poe_Part1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }  // Add this DbSet for Claims
    }
}
