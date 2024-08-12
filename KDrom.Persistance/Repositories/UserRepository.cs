using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task RemoveAsync(Guid id)
    {
        var uwerDb = await _context.Users.FindAsync(id);

        if (uwerDb is null)
            throw new InnerException("Пользователь не найден");

        _context.Users.Remove(uwerDb);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users;
    }

    public void UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> IsLoginTaken(string login)
    {
        return await _context.Users.AnyAsync(x => x.Login == login);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public Task<User?> GetByEmailWithRole(string email)
    {
        return _context.Users
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public Task<User?> GetByLoginWithRole(string login)
    {
        return _context.Users
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Login == login);
    }
}
