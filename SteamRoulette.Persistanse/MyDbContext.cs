using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain;

namespace SteamRoulette.Persistanse
{
    public class MyDbContext : IdentityDbContext<SteamUser, IdentityRole<int>, int>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
            //  base.Database.EnsureDeleted();
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