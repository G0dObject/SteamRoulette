using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SteamRoulette.Domain;
using SteamRoulette.Infrastructure.Intefaces.Services;
using SteamRoulette.WebApi.Services;
using System.Security.Claims;

namespace SteamRoulette.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration, UserManager<SteamUser> userManager, SignInManager<SteamUser> signInManager, UserService userService, IJwtTokenGenerator jwtTokenGenerator) : ControllerBase
    {

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("TestAuth")]
        public IActionResult Test()
        {
            if (User.Identity.IsAuthenticated)
                return Ok();
            return Unauthorized();
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
                var user = await userService.Authorize(authenticateResult);

                List<Claim> authClaims =
                [
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.SteamUserId.ToString()),
                    new Claim(ClaimTypes.UserData, user.ImgUrl)
                ];
                var token = jwtTokenGenerator.GenerateJwtToken(authClaims);
                return Redirect(returnUrl + "token=" + token);
            }
            else
            {
                return StatusCode(401);
            }
        }
    }
}