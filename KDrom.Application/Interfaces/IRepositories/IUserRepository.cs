using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailWithRole(string email);

    Task<User?> GetByLoginWithRole(string login);
}
