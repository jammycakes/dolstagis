using System.Net.Mail;
using System.Net.Mime;

namespace Dolstagis.Core.Mail
{
    public class SystemNetMailMailer : IMailer
    {
        private IMailSettings settings;

        public SystemNetMailMailer(IMailSettings settings)
        {
            this.settings = settings;
        }

        private MailAddress CreateMailAddress(IMailable mailable)
        {
            return new MailAddress(mailable.EmailAddress, mailable.Name);
        }

        private MailMessage CreateMailMessage(IMailable recipient, Message message)
        {
            var msg = new MailMessage();
            msg.To.Add(CreateMailAddress(recipient));
            if (message.Html == null) {
                msg.Body = message.Text;
                msg.IsBodyHtml = false;
            }
            else if (message.Text == null) {
                msg.Body = message.Html;
                msg.IsBodyHtml = true;
            }
            else {
                msg.Body = message.Html;
                var alternate = AlternateView.CreateAlternateViewFromString(message.Html);
                alternate.ContentType = new ContentType("text/html");
                msg.AlternateViews.Add(alternate);
            }
            return msg;
        }

        public void Send(IMailable recipient, Message message)
        {
            Send(recipient, message, null);
        }


        public void Send(IMailable recipient, Message message, IMailable sender)
        {
            var msg = CreateMailMessage(recipient, message);
            if (sender != null)
                msg.From = CreateMailAddress(sender);
            else
                msg.From = settings.GetSenderMailAddress();
            using (var client = new SmtpClient()) {
                client.Send(msg);
            }
        }
    }
}
