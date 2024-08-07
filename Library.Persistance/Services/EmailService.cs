using Library.Domain.Dto;
using Library.Domain.Services;
using Library.Persistance.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Library.Persistance.Services
{
	public class EmailService : IEmailService
	{
		private readonly SmtpOptions _smtpSettings;
        
		public EmailService(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpSettings = smtpOptions.Value;
        }
        
		public async Task SendAsync(EmailDto email)
		{
			using (var emailMessage = new MimeMessage())
			{
				emailMessage.From.Add(new MailboxAddress(string.Empty, _smtpSettings.Login));
				emailMessage.To.Add(new MailboxAddress(string.Empty, email.RecipientEmail));
				emailMessage.Subject = email.Subject;
				emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
				{
					Text = email.Message
				};

				using (var client = new SmtpClient())
				{
					await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, true);
					await client.AuthenticateAsync(_smtpSettings.Login, _smtpSettings.Password);
					await client.SendAsync(emailMessage);
				};
			}
		}
	}
}
