
namespace Dolstagis.Framework.Mail
{
    public class Message
    {
        /// <summary>
        ///  The subject of the mail message.
        /// </summary>

        public string Subject { get; set; }

        /// <summary>
        ///  The HTML content of the mail message.
        /// </summary>

        public string Html { get; set; }

        /// <summary>
        ///  The text content of the mail message.
        /// </summary>

        public string Text { get; set; }
    }
}
