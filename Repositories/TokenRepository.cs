using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Repositories;

public class TokenRepository : ITokenRepository
{
    private IConfiguration configuration;

    public TokenRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string CreateJWTToken(IdentityUser user, List<string> roles)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(configuration["JWT:Issuer"], configuration["JWT:Audience"], claims, expires: DateTime.Now.AddDays(3), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}