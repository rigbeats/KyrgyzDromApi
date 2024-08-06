using KDrom.Domain.Interfaces.IRepositories;
using Library.Domain.Entities;
using Library.Persistance;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly AppDbContext _context;

        public VerificationCodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VerificationCode verificationCode)
        {
            await _context.AddAsync(verificationCode);
        }

        public async Task<VerificationCode?> GetByUserIdAsync(string id)
        {
            return await _context.UserVerificationCodes.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
