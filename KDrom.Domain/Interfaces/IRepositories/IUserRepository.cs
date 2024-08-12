using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailWithRole(string email);

    Task<User?> GetByLoginWithRole(string login);

    void UpdateAsync(User user);

    Task RemoveAsync(Guid id);

    IQueryable<User> GetAll();

    Task<bool> IsEmailTaken(string email);

    Task<bool> IsLoginTaken(string login);

    Task SaveChangesAsync();
}
