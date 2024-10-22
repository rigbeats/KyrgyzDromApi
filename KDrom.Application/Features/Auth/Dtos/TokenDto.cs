namespace KDrom.Application.MediatR.Auth.Dtos;

public class TokenDto
{
    public string AccessToken { get; set; }

    public DateTime AccessTokenExpiry { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiry { get; set; }
}
