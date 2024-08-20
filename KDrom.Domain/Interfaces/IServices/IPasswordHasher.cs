using KDrom.Domain.Models;

namespace KDrom.Domain.Interfaces.IServices;

public interface IPasswordHasher
{
    HashedPasswordModel Hash(string password);

    bool Verify(string password, string salt, string hashedPassword);
}
