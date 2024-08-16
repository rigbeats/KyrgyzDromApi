using KDrom.Domain.Interfaces.IServices;
using KDrom.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace KDrom.Persistance.Services;

public class PasswordHasher : IPasswordHasher
{
    public HashedPasswordDto Hash(string password)
    {
        var salt = GenerateSalt();
        var saltedPassword = password + salt;
        var passwordHash = Sha256Hash(saltedPassword);

        return new HashedPasswordDto()
        {
            PasswordHash = passwordHash,
            Salt = salt,
        };
    }

    public bool Verify(string password, string salt, string hashedPassword)
    {
        //KeyDerivation.Pbkeydf2
        var saltedPassword = password + salt;
        var computeHash = Sha256Hash(saltedPassword);

        return computeHash == hashedPassword;
    }

    private string GenerateSalt()
    {
        var saltBytes = new byte[16];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }

    private string Sha256Hash(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        var sBuilder = new StringBuilder();

        foreach (var b in bytes)
            sBuilder.Append(b.ToString("X2"));

        return sBuilder.ToString();
    }
}
