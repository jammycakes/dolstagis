using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dolstagis.Web.Helpers.Flash
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
