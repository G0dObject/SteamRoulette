using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Persistanse;
using SteamRoullete.WebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SteamRoullete.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(JwtTokenGenerator jwtTokenGenerator, MyDbContext db, UserManager<IdentityUser> userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> Login([FromBody] LoginTransferObject model)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                bool currect = await _userManager.CheckPasswordAsync(user, model.Password);

                if (currect)
                {
                    List<Claim> authClaims = new()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier.ToString(), user.Id.ToString())
                    };

                    JwtSecurityToken? token = _jwtTokenGenerator.GenerateJwtToken(authClaims);

                    return new LoginResponseTransferObject(token, user.UserName, (user.Id));
                }
            }
            return StatusCode(401);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterTransferObject model)
        {
            IdentityUser userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Alredy exist");
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            return !result.Succeeded
                ? StatusCode(StatusCodes.Status400BadRequest, "Create Failed")
                : Ok("User created successfully!");
        }

        [HttpGet]
        [Route("Test")]
        [Authorize]
        public string Test()
        {
            return "Ok";
        }
    }
}