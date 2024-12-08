using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain;
using SteamRoulette.Domain.Common;

namespace SteamRoulette.Persistanse
{
    public class SteamDbContext : IdentityDbContext<SteamUser, IdentityRole<int>, int>
    {
        public DbSet<SteamItem> SteamItems { get; set; }
        public DbSet<Round> RoundsHistory { get; set; }
        public DbSet<RoundBet> RoundBets { get; set; }
        public DbSet<SteamUser> SteamUsers { get; set; }

        public SteamDbContext(DbContextOptions options) : base(options)
        {
            base.Database.EnsureDeleted();
            base.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = db.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}