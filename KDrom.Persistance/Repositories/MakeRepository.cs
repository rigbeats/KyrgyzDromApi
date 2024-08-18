using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories
{
    public class MakeRepository : IMakeRepository
    {
        private readonly ApplicationDbContext _context;

        public MakeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Make make)
        {
            await _context.Makes.AddAsync(make);
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _context.Makes
                .AnyAsync(x => x.Name.Clean() == name.Clean());
        }

        public async Task<Make?> Find(string id)
        {
            return await _context.Makes.FindAsync(id);
        }

        public async Task<Make?> FindByNameAsync(string name)
        {
            return await _context.Makes
                .SingleOrDefaultAsync(x => x.Name.Clean() == name.Clean());
        }

        public async Task<IEnumerable<Make>> GetAll()
        {
            return await _context.Makes.AsNoTracking().ToListAsync();
        }

        public IQueryable<Make> AsQueryble()
        {
            return _context.Makes.AsNoTracking();
        }

        public async Task RemoveAsync(string id)
        {
            var makeDb = await _context.Makes.FindAsync(id)
                ?? throw new InnerException("Марка не найдена");

            _context.Makes.Remove(makeDb);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
