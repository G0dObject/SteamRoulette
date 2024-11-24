using System.IdentityModel.Tokens.Jwt;

public class LoginResponseTransferObject
{
    public LoginResponseTransferObject(JwtSecurityToken token, string userName, string id)
    {
        Token = new JwtSecurityTokenHandler().WriteToken(token);
        UserName = userName;
        UserId = id;
    }

    public string Token { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
}