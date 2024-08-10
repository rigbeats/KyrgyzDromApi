namespace KDrom.Domain.Models;

public class HashedPasswordDto
{
    public string PasswordHash { get; set; }

    public string Salt { get; set; }
}
