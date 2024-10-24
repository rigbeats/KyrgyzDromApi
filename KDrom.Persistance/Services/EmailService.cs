﻿using KDrom.Domain.Interfaces.Services;
using KDrom.Persistance.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace KDrom.Persistance.Services;

public class EmailService : IEmailService
{
    private readonly SmtpOptions _smtpSettings;

    public EmailService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpSettings = smtpOptions.Value;
    }

    public async Task SendAsync(string email, string subject, string body)
    {
        using (var emailMessage = new MimeMessage())
        {
            emailMessage.From.Add(new MailboxAddress(string.Empty, _smtpSettings.Login));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
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
