using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IServices;
using KDrom.Domain.Models;
using KDrom.Persistance.Configuration;
using KDrom.Utilities;
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

    public TokenModel GenerateAccessToken(User user)
    {
        List<Claim> claims = [new("userId", user.Id.ToString())];
        claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
            SecurityAlgorithms.HmacSha256);

        var expiryDateTime = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiryMinutes);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: expiryDateTime);

        return new TokenModel()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpiry = expiryDateTime
        };
    }

    public TokenModel GenerateRefreshToken(User user)
    {
        var refreshToken = Generator.GenerateRandomString(64);
        var expiryDateTime = DateTime.UtcNow.AddMinutes(_options.RefreshTokenExpiryMinutes);

        return new TokenModel()
        {
            Token = refreshToken,
            TokenExpiry = expiryDateTime
        };
    }
}
