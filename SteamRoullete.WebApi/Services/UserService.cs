using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SteamRoulette.Domain;
using System.Security.Claims;

namespace SteamRoullete.WebApi.Services
{
    public class UserService
    {
        private readonly UserManager<SteamUser> _userManager;
        private readonly SignInManager<SteamUser> _signInManager;
        private readonly ILogger _logger;
        private readonly SteamService _steamService;

        public UserService(UserManager<SteamUser> userManager, SignInManager<SteamUser> signInManager, SteamService steamService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _steamService = steamService;
        }

        public async Task Authorize(AuthenticateResult authenticateResult)
        {
            var _steamUserIdUrl = authenticateResult.Principal.Claims.First(f => f.Type == ClaimTypes.NameIdentifier).Value;
            var _steamUserId64 = _steamUserIdUrl.Split('/').Last();

            var user = await _userManager.FindByNameAsync(_steamUserId64);
            if (user == null)
            {
                await CreateUser(authenticateResult, _steamUserId64);
            }
        }

        private async Task CreateUser(AuthenticateResult authenticateResult, string steamId64)
        {
            var _steamUsername = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);

            var imgUrl = await _steamService.GetProfileImageUrlAsync(steamId64);

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
        }
    }
}