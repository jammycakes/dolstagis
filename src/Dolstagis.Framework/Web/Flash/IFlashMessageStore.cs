using System.Collections.Generic;

namespace Dolstagis.Framework.Web.Flash
{
    public interface IFlashMessageStore
    {
        void AddMessage(IDictionary<string, object> tempData, FlashMessage message);
        IEnumerable<FlashMessage> GetMessages(IDictionary<string, object> tempData);
    }
}
