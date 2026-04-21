using EmailRegistrationApp.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailRegistrationApp.Services
{
    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<bool> SendEmailAsync(EmailRequest emailRequest)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", emailRequest.ToEmail));
                message.Subject = emailRequest.Subject;

                var bodyBuilder = new BodyBuilder();
                if (emailRequest.IsHtml)
                    bodyBuilder.HtmlBody = emailRequest.Body;
                else
                    bodyBuilder.TextBody = emailRequest.Body;

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port,
                        _smtpSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
                    
                    if (!string.IsNullOrEmpty(_smtpSettings.Username))
                    {
                        var sanitizedPassword = _smtpSettings.Password?.Replace(" ", "");
                        Console.WriteLine($"[EmailService] Attempting SMTP Auth for: {_smtpSettings.Username} (Pwd Length: {sanitizedPassword?.Length})");
                        await client.AuthenticateAsync(_smtpSettings.Username, sanitizedPassword);
                    }
                    
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"[EmailService] Sending failed to {emailRequest.ToEmail}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[EmailService] Inner Exception: {ex.InnerException.Message}");
                }
                return false;
            }
        }
    }
}