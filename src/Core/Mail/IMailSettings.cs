using System.Net.Mail;

namespace Dolstagis.Core.Mail
{
    public interface IMailSettings
    {
        MailAddress GetSenderMailAddress();
        string SenderEmail { get; }
        string SenderName { get; }
    }
}
