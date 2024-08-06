using KDrom.Domain.Interfaces.IRepositories;
using Library.Domain.Entities;
using Library.Persistance;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories
{
    public class UserVerificationCodeRepository : IUserVerificationCodeRepository
    {
        private readonly AppDbContext _context;

        public UserVerificationCodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserVerificationCode verificationCode)
        {
            await _context.AddAsync(verificationCode);
        }

        public async Task<UserVerificationCode?> GetByUserIdAsync(string id)
        {
            return await _context.UserVerificationCodes.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
