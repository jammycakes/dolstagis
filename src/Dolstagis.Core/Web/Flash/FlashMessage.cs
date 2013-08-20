using System;
using System.Web;

namespace Dolstagis.Framework.Web.Flash
{
    [Serializable]
    public class FlashMessage
    {
        public IHtmlString Message { get; private set; }

        public Level Level { get; private set; }

        public FlashMessage(IHtmlString message, Level level)
        {
            this.Message = message;
            this.Level = level;
        }
    }
}
