using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IServices;
using KDrom.Persistance.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KDrom.Persistance.Services;

public class JwtProvider : IJwtProvider
{
    private readonly JwtTokenOptions _options;

    public JwtProvider(IOptions<JwtTokenOptions> jwtTokenOptions)
    {
        _options = jwtTokenOptions.Value;
    }

    public string GenerateToken(User user)
    {
        Claim[] claims = [new("userId", user.Id.ToString())];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
