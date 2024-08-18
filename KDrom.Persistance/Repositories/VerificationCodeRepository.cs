using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class VerificationCodeRepository : IVerificationCodeRepository
{
    private readonly ApplicationDbContext _context;

    public VerificationCodeRepository(ApplicationDbContext context)
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
