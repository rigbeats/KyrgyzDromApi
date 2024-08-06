using Library.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories
{
    public interface IUserVerificationCodeRepository
    {
        Task AddAsync(UserVerificationCode verificationCode);

        Task<UserVerificationCode?> GetByUserIdAsync(string id);

        Task SaveChangesAsync();
    }
}
