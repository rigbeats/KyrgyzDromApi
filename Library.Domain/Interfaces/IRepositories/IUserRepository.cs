using Library.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<User?> GetByIdAsync(int id);
        
        void UpdateAsync(User user);

        Task RemoveAsync(int id);

        IQueryable<User> GetAll();

        Task<bool> IsEmailTaken(string email);

        Task<bool> IsLoginTaken(string login);

        Task SaveChangesAsync();
    }
}
