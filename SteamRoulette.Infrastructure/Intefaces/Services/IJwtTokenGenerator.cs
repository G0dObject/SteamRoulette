using System.Security.Claims;

namespace SteamRoulette.Infrastructure.Intefaces.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(List<Claim> claims);
    }
}