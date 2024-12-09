using Microsoft.IdentityModel.Tokens;
using SteamRoulette.Infrastructure.Intefaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SteamRoulette.WebApi.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration) => _configuration = configuration;

        public string GenerateJwtToken(List<Claim> claims)
        {
            SymmetricSecurityKey? authSigningKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            JwtSecurityToken? token = new(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddSeconds(123123132),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
