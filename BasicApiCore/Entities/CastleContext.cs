using Microsoft.EntityFrameworkCore;

namespace BasicApiCore.Entities
{
    public class CastleContext : DbContext
    {
        public CastleContext(DbContextOptions<CastleContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Castle> Castle { get; set; }
        public DbSet<CastleDetail> CastleDetail { get; set; }
    }
}
