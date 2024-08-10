using KDrom.Domain.Models;

namespace KDrom.Domain.Interfaces.IServices;

public interface IPasswordHasher
{
    HashedPasswordDto Hash(string password);

    bool Verify(string password, string salt, string hashedPassword);
}
