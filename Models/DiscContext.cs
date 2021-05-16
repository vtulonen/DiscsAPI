using Microsoft.EntityFrameworkCore;

namespace DiscsAPI.Models
{
    public class DiscContext : DbContext
    {
        public DiscContext(DbContextOptions<DiscContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Disc> Discs { get; set; } // Collection of disc entities
    }
}
