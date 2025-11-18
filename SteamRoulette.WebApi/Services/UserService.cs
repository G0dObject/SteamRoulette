using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SteamRoulette.Domain;
using SteamRoulette.Persistence;
using System.Security.Claims;

namespace SteamRoulette.WebApi.Services
{
    public class UserService
    {
        private readonly UserManager<SteamUser> _userManager;
        private readonly SignInManager<SteamUser> _signInManager;
        private readonly SteamService _steamService;

        public UserService(UserManager<SteamUser> userManager, SignInManager<SteamUser> signInManager, SteamService steamService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _steamService = steamService;
        }

        public async Task<SteamUser> Authorize(AuthenticateResult authenticateResult)
        {
            if (authenticateResult.Principal == null)
            {
                throw new InvalidOperationException("Authentication principal is null");
            }

            var steamUserIdClaim = authenticateResult.Principal.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier);
            if (steamUserIdClaim == null || string.IsNullOrEmpty(steamUserIdClaim.Value))
            {
                throw new InvalidOperationException("Steam user ID claim not found");
            }

            var _steamUserIdUrl = steamUserIdClaim.Value;
            var _steamUserId64 = _steamUserIdUrl.Split('/').Last();

            var user = await _userManager.FindBySteamIdAsync(_steamUserId64);

            if (user == null)
            {
                return await CreateUser(authenticateResult, _steamUserId64);
            }
            else
            {
                await SignInUser(user);
                return user;
            }
        }
        private async Task<SteamUser> CreateUser(AuthenticateResult authenticateResult, string steamId64)
        {
            if (authenticateResult.Principal == null)
            {
                throw new InvalidOperationException("Authentication principal is null");
            }

            var _steamUsername = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name) ?? "UnknownUser";

            string imgUrl = string.Empty;
            try
            {
                imgUrl = await _steamService.GetProfileImageUrlAsync(steamId64);
            }
            catch (Exception)
            {
                // Если не удалось получить изображение, продолжаем без него
                imgUrl = string.Empty;
            }

            var newUser = new SteamUser.Builder()
                .WithUsername(_steamUsername)
                .WithSteamId(steamId64)
                .WithImgUrl(imgUrl)
                .WithLastSeen(DateTime.Now)
                .Build();

            var result = await _userManager.CreateAsync(newUser);
            if (!result.Succeeded)
            {
                // Handle error
            }

            await _signInManager.SignInAsync(newUser, isPersistent: false);
            return newUser;
        }
        private async Task SignInUser(SteamUser steamUser)
        {
            await _signInManager.SignInAsync(steamUser, isPersistent: false);
        }
    }
}