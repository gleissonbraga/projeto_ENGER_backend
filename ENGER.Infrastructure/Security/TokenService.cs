using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ENGER.Domain.Entities;
using ENGER.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ENGER.Infrastructure.Security;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user, int subscriptionTypeId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var secretKey = Environment.GetEnvironmentVariable("JWT_KEY");
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("CompanyId", user.CompanyId.ToString()),
                new Claim("AdminLevel", ((int)user.Admin).ToString()),
                new Claim("subscriptionTypeId", (subscriptionTypeId).ToString()),
                new Claim("UserId", (user.UserId).ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(8), // Tempo de validade do crachá
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}