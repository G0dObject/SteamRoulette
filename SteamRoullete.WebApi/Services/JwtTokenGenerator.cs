using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SteamRoullete.WebApi.Services
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Generate a JwtSecurityToken using the provided authClaims
        public JwtSecurityToken GenerateJwtToken(List<Claim> authClaims)
        {
            //Create a SymmetricSecurityKey using the Jwt:Key from the configuration
            SymmetricSecurityKey? authSigningKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //Create a new JwtSecurityToken using the issuer, audience, expiration, claims, and signing credentials from the configuration
            JwtSecurityToken? token = new(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddSeconds(double.Parse(_configuration["Jwt:TokenLifeTime"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            //Return the JwtSecurityToken
            return token;
        }
    }
}