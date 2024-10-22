using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IUserVerificationCodeRepository : IRepository<UserVerificationCode>
{
    Task<UserVerificationCode?> GetByUserIdAsync(string userId);
}
