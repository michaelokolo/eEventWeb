using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly SmtpSettings _settings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IOptions<SmtpSettings> settings, ILogger<EmailSender> logger)
    {
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        _logger = logger;
    }
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            _logger.LogInformation("Sending email to {Email} with subject {Subject}", email, subject);
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {

                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };
            var mail = new MailMessage(_settings.From, email, subject, message)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
            _logger.LogInformation("Email sent successfully to {Email}", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {Email}", email);
            throw;
        }
        
    }


    public class SmtpSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string From { get; set; } = "";
        public bool EnableSsl { get; set; }
    }
    
}

