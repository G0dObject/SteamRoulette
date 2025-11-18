using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain;

namespace SteamRoulette.Persistence
{
    public static class UserManagerExtensions
    {
        public static async Task<TUser?> FindBySteamIdAsync<TUser>(this UserManager<TUser> userManager, string steamId) where TUser : SteamUser
        {
            return await userManager.Users.FirstOrDefaultAsync(u => u.SteamUserId == steamId);
        }
    }
}

