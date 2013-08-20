using System.Net.Mail;
using Dolstagis.Framework.Configuration;

namespace Dolstagis.Framework.Mail
{
    public class MailSettings : Settings, IMailSettings
    {
        public string SenderName { get; private set; }

        public string SenderEmail { get; private set; }

        public MailAddress GetSenderMailAddress()
        {
            return new MailAddress(this.SenderEmail, this.SenderName);
        }
    }
}
