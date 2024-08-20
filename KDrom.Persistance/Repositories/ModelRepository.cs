using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly ApplicationDbContext _context;

    public ModelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Model model)
    {
        await _context.Models.AddAsync(model);
    }

    public async Task<Model?> Find(string id)
    {
        return await _context.Models.FindAsync(id);
    }

    public async Task<IEnumerable<Model>> GetAll()
    {
        return await _context.Models.AsNoTracking().ToListAsync();
    }

    public IQueryable<Model> AsQueryble()
    {
        return _context.Models.AsNoTracking();
    }

    public async Task<IEnumerable<Model>> GetAllByMake(Make make)
    {
        return await _context.Models.AsNoTracking()
            .Where(x => x.Make == make).ToListAsync();
    }

    public void Remove(Model model)
    {
        _context.Models.Remove(model);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
