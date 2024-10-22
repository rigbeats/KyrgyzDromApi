using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken>
{
    Task<UserRefreshToken?> GetByToken(string token);

    Task<UserRefreshToken?> GetByUserIdAsync(string userId);
}
