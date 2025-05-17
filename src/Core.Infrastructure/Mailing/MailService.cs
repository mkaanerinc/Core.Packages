using Core.CrossCuttingConcerns.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mailing;

/// <summary>
/// Implementation of the email sending service.
/// Handles SMTP configuration and error management.
/// </summary>
public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly ILoggerService _loggerServiceBase;

    /// <summary>
    /// Initializes a new instance of the MailService class.
    /// </summary>
    /// <param name="mailSettings">SMTP settings injected via IOptions.</param>
    /// <param name="logger">Logger instance for logging email operations.</param>
    public MailService(IOptions<MailSettings> mailSettings, ILoggerService loggerServiceBase)
    {
        _mailSettings = mailSettings.Value;
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Sends an email to multiple recipients.
    /// </summary>
    /// <param name="to">The list of recipient email addresses.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email (supports HTML).</param>
    /// <param name="isBodyHtml">Indicates whether the body is in HTML format.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendBulkEmailAsync(string[] to, string subject, string body, bool isBodyHtml = true)
    {
        if (to == null || to.Length == 0 || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("Recipient list, subject, or body cannot be empty.", nameof(to));
        }

        try
        {
            using var client = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
            {
                EnableSsl = _mailSettings.EnableSsl,
                Credentials = new System.Net.NetworkCredential(_mailSettings.Username, _mailSettings.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            };

            foreach (var recipient in to)
            {
                if (!string.IsNullOrWhiteSpace(recipient))
                {
                    mailMessage.To.Add(recipient);
                }
            }

            _loggerServiceBase.LogInformation($"Sending bulk email to: {to.Length} recipients, Subject: {subject}");
            await client.SendMailAsync(mailMessage);
            _loggerServiceBase.LogInformation($"Bulk email sent successfully to: {to.Length} recipients");
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Failed to send bulk email, Error: {ex.Message}", ex);
            throw new InvalidOperationException("An error occurred while sending the bulk email.", ex);
        }
    }

    /// <summary>
    /// Sends an email to the specified recipient.
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email (supports HTML).</param>
    /// <param name="isBodyHtml">Indicates whether the body is in HTML format.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("Email address, subject, or body cannot be empty.", nameof(to));
        }

        try
        {
            using var client = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
            {
                EnableSsl = _mailSettings.EnableSsl,
                Credentials = new System.Net.NetworkCredential(_mailSettings.Username, _mailSettings.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            };

            mailMessage.To.Add(to);

            _loggerServiceBase.LogInformation($"Sending email to: {to}, Subject: {subject}");
            await client.SendMailAsync(mailMessage);
            _loggerServiceBase.LogInformation($"Email sent successfully to: {to}");
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Failed to send email to: {to}, Error: {ex.Message}", ex);
            throw new InvalidOperationException("An error occurred while sending the email.", ex);
        }
    }
}
