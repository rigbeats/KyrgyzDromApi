using System.Linq.Expressions;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IRepository<T> where T : class
{
    Task<T?> FindAsync(string id);

    Task AddAsync(T entity);

    void Update(T entity);

    IQueryable<T> GetSet();

    Task<IEnumerable<T>> GetAll();

    void Remove(T entity);

    Task SaveChangesAsync();

    Task<bool> IsAnyAsync(Expression<Func<T, bool>> predicate);

    IQueryable Where(Expression<Func<T, bool>> predicate);
}
