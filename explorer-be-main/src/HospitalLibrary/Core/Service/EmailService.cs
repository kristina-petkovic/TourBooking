using System;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service.IService;
using HospitalLibrary.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace HospitalLibrary.Core.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly SmtpClient _smtpClient;

        public EmailService(IOptions<MailSettings> mailSettings, SmtpClient smtpClient)
        {
            _mailSettings = mailSettings.Value;
            _smtpClient = smtpClient;
        }
        
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> PurchaseMail(Purchase p, User user)
        {

            try
            {
                var mailText = "Hello [username],\n\n" +
                               "You made purchase at [date] for tour : [tourName] ;"+
                               "Bought tickets : [count] "+
                    "Price  : [price] $ ";
                mailText = mailText.Replace("[username]", user.FirstName);
                mailText = mailText.Replace("[date]", p.PurchaseDate.ToString());
                mailText = mailText.Replace("[tourName]", p.TourName);
                mailText = mailText.Replace("[count]", p.Count.ToString());
                mailText = mailText.Replace("[price]", p.Price.ToString());

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = $"Hello {user.FirstName}";

                var builder = new BodyBuilder();
                builder.HtmlBody = mailText;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendIssueEMail(Issue issue, User user)
        {
          try
          {
              var mailText = "Hello [username],\n\n" +
                             "New issue with ID [id] about tour [tour] arrived: [text] ";
              mailText = mailText.Replace("[username]", user.FirstName);
              mailText = mailText.Replace("[id]", issue.Id.ToString());
              mailText = mailText.Replace("[tour]", issue.TourId.ToString());
              mailText = mailText.Replace("[text]", issue.Text);

              var email = new MimeMessage();
              email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
              email.To.Add(MailboxAddress.Parse(user.Email));
              email.Subject = $"Hello {user.FirstName}";

              var builder = new BodyBuilder();
              builder.HtmlBody = mailText;
              email.Body = builder.ToMessageBody();

              using var smtp = new SmtpClient();
              await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
              await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
              await smtp.SendAsync(email);
              await smtp.DisconnectAsync(true);

              return true;
          }
          catch (Exception ex)
          {
              Console.WriteLine($"Failed to send email: {ex.Message}");
              return false;
          }
        }

        public async Task<bool> SendBlockedMail(User user)
        {
            try
            {
                var mailText = "Hello [username],\n\n" +
                               "Your account has been blocked. ";
                mailText = mailText.Replace("[username]", user.FirstName);

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = $"Hello {user.FirstName}";

                var builder = new BodyBuilder();
                builder.HtmlBody = mailText;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }

        }

        public async Task<bool> NotifyTouristAboutNewTour(User user, string name)
        {
            try
            {
                var mailText = "Hello [username],\n\n" +
                               "New tour [name] you might like, has been added.Check it out ";
                mailText = mailText.Replace("[username]", user.FirstName);
                mailText = mailText.Replace("[name]", name);

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = $"Hello {user.FirstName}";

                var builder = new BodyBuilder();
                builder.HtmlBody = mailText;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
        }
    }
}