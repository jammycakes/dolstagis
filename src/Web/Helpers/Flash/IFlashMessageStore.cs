using System;
using System.Collections;
using System.Collections.Generic;

namespace Dolstagis.Web.Helpers.Flash
{
    interface IFlashMessageStore
    {
        void AddMessage(IDictionary<string, object> tempData, FlashMessage message);
        IEnumerable<FlashMessage> GetMessages(IDictionary<string, object> tempData);
    }
}
