using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class UserRefreshTokenRepository : IUserRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public UserRefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserRefreshToken token)
    {
        await _context.UserRefreshTokens.AddAsync(token);
    }

    public async Task<UserRefreshToken?> GetByToken(string token)
    {
        return await _context.UserRefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task<UserRefreshToken?> GetByUserIdAsync(string id)
    {
        return await _context.UserRefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(x => x.UserId == id);
    }

    public async Task RemoveAsync(string id)
    {
        var tokenDb = await _context.UserRefreshTokens.FindAsync(id)
            ?? throw new InnerException("Невалидный токен, повторно пройдите авторизацию");

        _context.UserRefreshTokens.Remove(tokenDb);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
