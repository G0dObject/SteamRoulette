using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain;
using SteamRoulette.Domain.Common;
using System.Runtime.Intrinsics.X86;

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
            // base.Database.EnsureDeleted();
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

            // Создаем пользователя на основе claims
            var user = new SteamUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 1,
                SteamUserId = "76561198374261740", // Уникальный идентификатор пользователя
                UserName = "Сержант", // Имя пользователя из claims
                Email = "user@example.com", // Укажите email (если требуется)
                ImgUrl = "https://avatars.steamstatic.com/46d3dc44a52ce13feeb11f9174a3df192e281d79_full.jpg", // URL аватара из claims
                Name = "Сержант", // Имя пользователя из claims
                CreatedAt = DateTime.UtcNow, // Время создания пользователя
                LastSeens = DateTime.UtcNow // Время последнего входа
                
            };

            modelBuilder.Entity<SteamUser>().HasData(user);

            // Seed data for SteamItem with relationships to SteamUser
            modelBuilder.Entity<SteamItem>().HasData(
                new SteamItem
                {
                    SteamItemId = 1,
                    SteamItemPrice = 10.99m,
                    SteamItemImg = "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
                    SteamItemTitle = "Item 1",
                    SteamUserId = 1 // Внешний ключ для связи с SteamUser
                },
                new SteamItem
                {
                    SteamItemId = 2,
                    SteamItemPrice = 15.99m,
                    SteamItemImg = "https://community.fastly.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpot7HxfDhnwMzJemkV09K_loKHkuXLPr7Vn35cppQm2L_F89v30ALtqEM_ZGyiI4GddQRraF3X_QLokubmh5-7uZ_Pm3J9-n51524MalI/360fx360f",
                    SteamItemTitle = "Item 2",
                    SteamUserId = 1 // Внешний ключ для связи с SteamUser
                }
                
            );
        }
    }
}