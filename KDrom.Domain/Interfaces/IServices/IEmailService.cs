using KDrom.Domain.Dto;

namespace KDrom.Domain.Interfaces.Services;

public interface IEmailService
{
    Task SendAsync(EmailDto email);
}
