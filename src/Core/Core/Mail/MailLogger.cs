using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Dolstagis.Core.Mail
{
    public class MailLogger : IMailer
    {
        Logger log = LogManager.GetCurrentClassLogger();

        public void Send(IMailable recipient, Message message)
        {
            Send(recipient, message, null);
        }

        public void Send(IMailable recipient, Message message, IMailable sender)
        {
            if (!log.IsInfoEnabled) return;
            log.Info("Recipient: {0} <{1}>", recipient.Name, recipient.EmailAddress);
            if (sender != null) {
                log.Info("Sender:    {0} <{1}>", sender.Name, sender.EmailAddress);
            }
            else {
                log.Info("Sender:    (not specified)");
            }
            log.Info("Subject:   " + message.Subject);
            if (message.Text != null) {
                log.Info(message.Text);
            }
            if (message.Html != null) {
                log.Info("HTML:      " + message.Html);
            }
        }
    }
}
