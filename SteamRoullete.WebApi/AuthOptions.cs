using Microsoft.IdentityModel.Tokens;
using System.Text;

public class AuthOptions
{
    public const string ISSUER = "My";
    public const string AUDIENCE = "My";
    private const string KEY = "99a52df3ff3d499488e2fa28150c4106a2cb5e928891a830a9aa3922b2d32160";
    public const int LIFETIME = 4124124;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}