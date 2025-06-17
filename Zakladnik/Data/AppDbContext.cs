using Microsoft.EntityFrameworkCore;
using Zakladnik.Models;

namespace Zakladnik.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Bet> Bets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>()
                .Property(z => z.Stake)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Bet>()
                .Property(z => z.Odds)
                .HasPrecision(10, 2);
        }
    }
}
