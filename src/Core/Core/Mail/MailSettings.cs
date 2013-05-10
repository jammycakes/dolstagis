using System.Net.Mail;
using Dolstagis.Core.Configuration;

namespace Dolstagis.Core.Mail
{
    public class MailSettings : AppConfigSettingsBase, IMailSettings
    {
        public string SenderName { get; private set; }

        public string SenderEmail { get; private set; }

        public MailAddress GetSenderMailAddress()
        {
            return new MailAddress(this.SenderEmail, this.SenderName);
        }
    }
}
