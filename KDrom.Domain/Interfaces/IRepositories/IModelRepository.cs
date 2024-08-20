using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAll();

        Task<IEnumerable<Model>> GetAllByMake(Make make);

        IQueryable<Model> AsQueryble();

        Task<Model?> Find(string id);

        Task AddAsync(Model model);

        void Remove(Model model);

        Task SaveChangesAsync();
    }
}
