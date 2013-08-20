
namespace Dolstagis.Framework.Mail
{
    /// <summary>
    ///  Represents an e-mail sending class.
    /// </summary>

    public interface IMailer
    {
        /// <summary>
        ///  Sends a mail message to a recipient from the system.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="message">The message to be sent.</param>

        void Send(IMailable recipient, Message message);

        /// <summary>
        ///  Sends a mail message to a recipient from another user.
        /// </summary>
        /// <param name="recipient">The recipient.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="sender">The sender.</param>

        void Send(IMailable recipient, Message message, IMailable sender);
    }
}
