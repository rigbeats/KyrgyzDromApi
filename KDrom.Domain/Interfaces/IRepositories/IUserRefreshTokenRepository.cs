using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IUserRefreshTokenRepository
{
    Task AddAsync(UserRefreshToken token);

    Task<UserRefreshToken?> GetByUserIdAsync(string id);

    Task<UserRefreshToken?> GetByToken(string token);

    Task RemoveAsync(string id);

    Task SaveChangesAsync();
}
