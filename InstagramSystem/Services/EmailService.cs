using InstagramSystem.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace InstagramSystem.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailFormDto emailForm);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly string HOST;
        private readonly string USERNAME;
        private readonly string PASSWORD;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
            HOST = _config.GetSection("Email:Host").Value??"";
            USERNAME = _config.GetSection("Email:Username").Value ?? "";
            PASSWORD = _config.GetSection("Email:Password").Value ?? "";
        }
        public void SendEmail(EmailFormDto emailForm)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:Username").Value));
            email.To.Add(MailboxAddress.Parse(emailForm.To));
            email.Subject = emailForm.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailForm.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(HOST, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(USERNAME, PASSWORD);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
