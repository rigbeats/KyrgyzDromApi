using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAll();

        Task<IEnumerable<Model>> GetAllByMakeIdAsync(string makeId);

        IQueryable<Model> AsQueryble();

        Task<Model?> FindAsync(string id);

        Task<bool> ExistByNameAndBrandId(string name, string makeId);

        Task AddAsync(Model model);

        void Remove(Model model);

        Task SaveChangesAsync();
    }
}
