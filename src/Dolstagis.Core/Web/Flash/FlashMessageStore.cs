using System.Collections.Generic;
using System.Linq;

namespace Dolstagis.Framework.Web.Flash
{
    public class FlashMessageStore : IFlashMessageStore
    {
        private const string tempDataKey = "{A6AEDAD9-FDA0-42ED-96BE-B90CAA8BC954}";

        public void AddMessage(IDictionary<string, object> tempData, FlashMessage message)
        {
            object obj;
            if (!tempData.TryGetValue(tempDataKey, out obj) || !(obj is IList<FlashMessage>)) {
                obj = new List<FlashMessage>();
                tempData[tempDataKey] = obj;
            }
            var messages = (IList<FlashMessage>)obj;
            messages.Add(message);
        }

        public IEnumerable<FlashMessage> GetMessages(IDictionary<string, object> tempData)
        {
            object obj;
            if (tempData.TryGetValue(tempDataKey, out obj) && (obj is IList<FlashMessage>))
                return (IList<FlashMessage>)obj;
            else
                return Enumerable.Empty<FlashMessage>();
        }
    }
}
