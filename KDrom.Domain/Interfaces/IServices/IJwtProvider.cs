using KDrom.Domain.Entities;
using KDrom.Domain.Models;

namespace KDrom.Domain.Interfaces.IServices;

public interface IJwtProvider
{
    TokenModel GenerateAccessToken(User user);

    TokenModel GenerateRefreshToken(User user);
}
