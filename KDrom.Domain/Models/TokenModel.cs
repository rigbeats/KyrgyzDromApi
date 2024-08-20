namespace KDrom.Domain.Models;

public class TokenModel
{
    public string Token { get; set; }
    
    public DateTime TokenExpiry { get; set; }
}
