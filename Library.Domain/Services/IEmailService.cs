using Library.Domain.Dto;

namespace Library.Domain.Services
{
	public interface IEmailService
	{
		Task SendAsync(EmailDto email);
	}
}
