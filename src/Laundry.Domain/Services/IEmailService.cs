namespace Laundry.Domain.Services;

public interface IEmailService
{
    Task SendEmailWithAttachment(string toEmail, string subject, string body, 
        MemoryStream attachment, string attachmentName);
}