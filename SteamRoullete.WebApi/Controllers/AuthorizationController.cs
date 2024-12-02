using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SteamRoulette.Domain;
using SteamRoullete.WebApi.Services;

namespace SteamRoullete.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<SteamUser> _userManager;
        private readonly SignInManager<SteamUser> _signInManager;
        private readonly UserService _userService;

        public AuthController(IConfiguration configuration, UserManager<SteamUser> userManager, SignInManager<SteamUser> signInManager, UserService userService)
        {
            _userService = userService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("Login")]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(HandleResponse), new { returnUrl })
            };

            return Challenge(authenticationProperties, "Steam");
        }

        //add validation
        [HttpGet("HandleResponse")]
        public async Task<IActionResult> HandleResponse(string returnUrl)
        {
            AuthenticateResult authenticateResult = await HttpContext.AuthenticateAsync("Steam");

            if (authenticateResult.Succeeded)
            {
                await _userService.Authorize(authenticateResult);
            }
            else
            {
                return StatusCode(401);
            }

            return Redirect(returnUrl);
        }
    }
}