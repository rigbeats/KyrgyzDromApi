using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<User?> GetByEmailWithRole(string email)
        => await Set
        .Include(x => x.Role)
        .SingleOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetByLoginWithRole(string login)
        => await Set
        .Include(x => x.Role)
        .SingleOrDefaultAsync(x => x.Login == login);
}
