using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mailing;

/// <summary>
/// Configuration settings for SMTP email delivery.
/// </summary>
public class MailSettings
{
    /// <summary>
    /// The SMTP server host name or IP address.
    /// </summary>
    public string Host { get; set; } = null!;

    /// <summary>
    /// The port number used to connect to the SMTP server.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Indicates whether SSL should be used for the SMTP connection.
    /// </summary>
    public bool EnableSsl { get; set; }

    /// <summary>
    /// The username used to authenticate with the SMTP server.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// The password used to authenticate with the SMTP server.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// The email address from which emails will be sent.
    /// </summary>
    public string SenderEmail { get; set; } = null!;

    /// <summary>
    /// The display name of the sender shown in the recipient's inbox.
    /// </summary>
    public string SenderName { get; set; } = null!;
}
