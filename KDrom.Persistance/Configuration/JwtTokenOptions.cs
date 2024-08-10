namespace KDrom.Persistance.Configuration;

public class JwtTokenOptions
{
    public string Secret { get; set; }

    public int AccessTokenExpiryMinutes { get; set; }

    public int RefreshTokenExpiryMinutes { get; set; }
}
