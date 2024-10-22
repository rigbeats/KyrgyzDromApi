using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class UserVerificationCodeRepository : RepositoryBase<UserVerificationCode>, IUserVerificationCodeRepository
{
    public UserVerificationCodeRepository(ApplicationDbContext context) : base(context) { }

    public Task<UserVerificationCode?> GetByUserIdAsync(string userId)
        => Set
        .FirstOrDefaultAsync(x => x.UserId == userId 
            && x.IsUsed == false);
}
