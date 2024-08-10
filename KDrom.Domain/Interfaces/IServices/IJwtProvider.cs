using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IServices;

public interface IJwtProvider
{
    string GenerateToken(User user);
}
