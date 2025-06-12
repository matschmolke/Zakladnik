using Microsoft.EntityFrameworkCore;
using Zakladnik.Models;

namespace Zakladnik.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Zaklad> Zaklady { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zaklad>()
                .Property(z => z.Stawka)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Zaklad>()
                .Property(z => z.Kurs)
                .HasPrecision(10, 2);
        }
    }
}
