using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Dolstagis.Web.Helpers.Flash;

namespace Dolstagis.Web.Helpers
{
    public static class FlashHelpers
    {
        public const Level DefaultLevel = Level.Info;

        /// <summary>
        ///  Flashes a plain text message to the user at the default message level.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message">
        ///  The message to be displayed to the user. This will be HTML encoded.
        ///  </param>

        public static void Flash(this ControllerBase controller, string message)
        {
            Flash(controller, new HtmlString(WebUtility.HtmlEncode(message)), DefaultLevel);
        }

        /// <summary>
        ///  Flashes a plain text message to the user at the specified message level.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message">
        ///  The message to be displayed to the user. This will be HTML encoded.
        /// </param>
        /// <param name="level">
        ///  The importance level of the message.
        /// </param>

        public static void Flash(this ControllerBase controller, string message, Level level)
        {
            Flash(controller, new HtmlString(WebUtility.HtmlEncode(message)), level);
        }

        /// <summary>
        ///  Flashes an HTML formatted message to the user.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message">
        ///  The message to be displayed to the user.
        /// </param>

        public static void Flash(this ControllerBase controller, IHtmlString message)
        {
            Flash(controller, message, DefaultLevel);
        }

        /// <summary>
        ///  Flashes an HTML formatted message to the user, at the specified message level.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message">
        ///  The message to be displayed to the user.
        /// </param>
        /// <param name="level">
        ///  The importance level of the message.
        /// </param>

        public static void Flash(this ControllerBase controller, IHtmlString message, Level level)
        {
            Flash(controller, new FlashMessage(message, level));
        }

        /// <summary>
        ///  Flashes a message to the user.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message">
        ///  The message to be displayed to the user.
        /// </param>

        public static void Flash(this ControllerBase controller, FlashMessage message)
        {
            var flashMessageStore = DependencyResolver.Current.GetService<IFlashMessageStore>();
            flashMessageStore.AddMessage(controller.TempData, message);
        }

        /// <summary>
        ///  Gets all the messages that are to be displayed to the user.
        /// </summary>
        /// <param name="html"></param>
        /// <returns>
        ///  A list of messages, sorted in the order in which they were added.
        /// </returns>

        public static IEnumerable<FlashMessage> GetFlashedMessages(this HtmlHelper html)
        {
            var flashMessageStore = DependencyResolver.Current.GetService<IFlashMessageStore>();
            return flashMessageStore.GetMessages(html.ViewContext.TempData);
        }

        /// <summary>
        ///  Gets an HTML string 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>

        public static IHtmlString FlashedMessages(this HtmlHelper html)
        {
            var messages = GetFlashedMessages(html);
            if (!messages.Any()) return new HtmlString(String.Empty);
            var sb = new StringBuilder();
            sb.Append("<ul id=\"flash\">");
            foreach (var msg in messages.OrderBy(x => x.Level)) {
                sb.AppendFormat(
                    "<li class=\"{0}\">{1}</li>",
                    msg.Level.ToString().ToLower(), msg.Message.ToHtmlString()
                );
            }
            sb.Append("</ul>");
            return new HtmlString(sb.ToString());
        }
    }
}
