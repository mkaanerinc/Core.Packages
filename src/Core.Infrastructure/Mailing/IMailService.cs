using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mailing;

/// <summary>
/// Defines the contract for email sending operations.
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Sends an email to the specified recipient.
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email (supports HTML).</param>
    /// <param name="isBodyHtml">Indicates whether the body is in HTML format.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true);

    /// <summary>
    /// Sends an email to multiple recipients.
    /// </summary>
    /// <param name="to">The list of recipient email addresses.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email (supports HTML).</param>
    /// <param name="isBodyHtml">Indicates whether the body is in HTML format.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendBulkEmailAsync(string[] to, string subject, string body, bool isBodyHtml = true);
}
