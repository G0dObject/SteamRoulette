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
            var jwtKey = _configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT:Key is not configured");
            var jwtIssuer = _configuration["JWT:Issuer"] ?? throw new InvalidOperationException("JWT:Issuer is not configured");
            var jwtAudience = _configuration["JWT:Audience"] ?? throw new InvalidOperationException("JWT:Audience is not configured");

            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(jwtKey));
            JwtSecurityToken token = new(
                issuer: jwtIssuer,
                audience: jwtAudience,
                expires: DateTime.Now.AddSeconds(123123132),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
