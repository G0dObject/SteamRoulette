using Microsoft.AspNetCore.Authentication;
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
    public class AuthController(UserService userService, IJwtTokenGenerator jwtTokenGenerator) : ControllerBase
    {
        [HttpGet("Login")]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(HandleResponse), new { returnUrl })
            };

            return Challenge(authenticationProperties, "Steam");
        }

        [HttpGet("HandleResponse")]
        public async Task<IActionResult> HandleResponse(string returnUrl)
        {
            try
            {
                AuthenticateResult authenticateResult = await HttpContext.AuthenticateAsync("Steam");

                if (authenticateResult.Succeeded)
                {
                    var user = await userService.Authorize(authenticateResult);

                    List<Claim> authClaims =
                    [
                        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                        new Claim(ClaimTypes.NameIdentifier, user.SteamUserId ?? string.Empty),
                        new Claim(ClaimTypes.UserData, user.ImgUrl ?? string.Empty)
                    ];
                    
                    var token = jwtTokenGenerator.GenerateJwtToken(authClaims);
                    
                    // Формируем URL редиректа
                    string redirectUrl;
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        // Если returnUrl не указан, используем дефолтный путь фронтенда
                        redirectUrl = $"http://localhost:5173/?token={Uri.EscapeDataString(token)}";
                    }
                    else
                    {
                        // Если returnUrl уже содержит query параметры, добавляем токен через &
                        // Иначе добавляем через ?
                        var separator = returnUrl.Contains('?') ? "&" : "?";
                        redirectUrl = $"{returnUrl}{separator}token={Uri.EscapeDataString(token)}";
                    }
                    
                    return Redirect(redirectUrl);
                }
                else
                {
                    var errorMessage = authenticateResult.Failure?.Message ?? "Authentication failed";
                    return BadRequest(new { error = "Authentication failed", details = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred during authentication", details = ex.Message });
            }
        }
    }
}