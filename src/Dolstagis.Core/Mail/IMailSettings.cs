using System.Net.Mail;

namespace Dolstagis.Framework.Mail
{
    public interface IMailSettings
    {
        MailAddress GetSenderMailAddress();
        string SenderEmail { get; }
        string SenderName { get; }
    }
}
