namespace KDrom.Domain.Models;

public class HashedPasswordModel
{
    public string PasswordHash { get; set; }

    public string Salt { get; set; }
}
