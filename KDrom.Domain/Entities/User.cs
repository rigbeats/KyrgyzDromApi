using KDrom.Domain.Enums;

namespace KDrom.Domain.Entities;

public class User : EntityBase
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }

    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public UserRole Role { get; set; }
}
