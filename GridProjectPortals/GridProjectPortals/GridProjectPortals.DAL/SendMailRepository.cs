using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GridProjectPortals.DAL
{
    public class SendMailRepository : ISendIMailRepository
    {
        private readonly string connectionString;
        private readonly MailSettings _mailSettings;
        private readonly IWebHostEnvironment _host;
        public SendMailRepository(IConfiguration configuration, IOptions<MailSettings> mailSettings, IWebHostEnvironment host)
        {
            connectionString = Convert.ToString(configuration.GetSection("ConnectionStrings:GRIDDb").Value);
            _mailSettings = mailSettings.Value;
            _host = host;
        }
       
        public async Task SendEmailAsync(sendMail sendMail)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(sendMail.MailTo));
            email.Subject = sendMail.Subject;
            var builder = new BodyBuilder();
            if (sendMail.Attachment != null)
            {
                byte[] fileBytes;
                foreach (var file in sendMail.Attachment)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = sendMail.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
