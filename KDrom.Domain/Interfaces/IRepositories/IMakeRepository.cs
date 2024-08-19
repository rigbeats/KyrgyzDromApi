using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories
{
    public interface IMakeRepository
    {
        Task AddAsync(Make make);

        Task<Make?> FindByNameAsync(string name);

        Task<Make?> Find(string id);

        Task<bool> ExistByNameAsync(string name);

        Task RemoveAsync(string id);

        Task<IEnumerable<Make>> GetAll();

        IQueryable<Make> AsQueryble();

        Task SaveChangesAsync();
    }
}
