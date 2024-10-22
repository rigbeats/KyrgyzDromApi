using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class UserRefreshTokenRepository : RepositoryBase<UserRefreshToken>, IUserRefreshTokenRepository
{
    public UserRefreshTokenRepository(ApplicationDbContext context) : base(context) { }

    public async Task<UserRefreshToken?> GetByToken(string token)
        => await Set
        .Include(x => x.User)
        .FirstOrDefaultAsync(x => x.Token == token);

    public async Task<UserRefreshToken?> GetByUserIdAsync(string userId)
        => await Set
        .Include(x => x.User)
        .FirstOrDefaultAsync(x => x.UserId == userId);
}
