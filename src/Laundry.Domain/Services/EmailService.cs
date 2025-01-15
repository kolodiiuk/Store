using System.Net;
using System.Net.Mail;
using System.IO;
using System.Threading.Tasks;

namespace Laundry.Domain.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUser = smtpUser;
        _smtpPass = smtpPass;
    }

    public async Task SendEmailWithAttachment(string toEmail, string subject, string body,
        MemoryStream attachment, string attachmentName)
    {
        try
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.EnableSsl = true;

                using (var mailMessage = new MailMessage
                       {
                           From = new MailAddress(_smtpUser),
                           Subject = subject,
                           Body = body,
                           IsBodyHtml = true,
                       })
                {
                    mailMessage.To.Add(toEmail);

                    if (attachment != null && attachment.Length > 0)
                    {
                        attachment.Position = 0; // Ensure the stream is at the start
                        using (var attachmentItem = new Attachment(attachment, attachmentName))
                        {
                            mailMessage.Attachments.Add(attachmentItem);
                            await client.SendMailAsync(mailMessage);
                        }
                    }
                    else
                    {
                        await client.SendMailAsync(mailMessage);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log exception or handle it as per your application's requirements
            throw new InvalidOperationException("Failed to send email", ex);
        }
    }
}