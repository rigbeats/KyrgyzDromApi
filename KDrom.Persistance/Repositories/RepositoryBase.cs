using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KDrom.Persistance.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    protected DbSet<T> Set => _context.Set<T>();

    public async Task AddAsync(T entity) 
        => await Set.AddAsync(entity);

    public async Task<T?> FindAsync(string id)
        => await Set.FindAsync(id);

    public void Update(T entity)
        => Set.Update(entity);

    public IQueryable<T> GetSet()
        => Set;

    public void Remove(T entity)
        => Set.Remove(entity);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task<bool> IsAnyAsync(Expression<Func<T, bool>> predicate)
        => await Set.AnyAsync(predicate);

    public IQueryable Where(Expression<Func<T, bool>> predicate)
        => Set.Where(predicate);

    public async Task<IEnumerable<T>> GetAll()
        => await Set
        .ToListAsync();
}
