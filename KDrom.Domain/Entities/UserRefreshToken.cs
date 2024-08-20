namespace KDrom.Domain.Entities;

public class UserRefreshToken : EntityBase
{
    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }

    public User User { get; set; }

    public string UserId { get; set; }

    public bool IsUsed { get; set; }
}
