namespace KDrom.Domain.Interfaces.Services;

public interface IEmailService
{
    Task SendAsync(string email, string subject, string body);
}
