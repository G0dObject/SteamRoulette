using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain;
using SteamRoulette.Domain.Common;

namespace SteamRoulette.Persistence
{
    public class SteamDbContext : IdentityDbContext<SteamUser, IdentityRole<int>, int>
    {
        public DbSet<SteamItem> SteamItems { get; set; }
        public DbSet<Round> RoundsHistory { get; set; }
        public DbSet<RoundBet> RoundBets { get; set; }
        public DbSet<SteamUser> SteamUsers { get; set; }

        public SteamDbContext(DbContextOptions<SteamDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = db.db");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

