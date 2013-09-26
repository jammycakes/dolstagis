using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Views;

namespace Dolstagis.Web.Static
{
    public class ResourceView : IView
    {
        private IResource _resource;
        private string _contentType;

        private const int _bufSize = 4 * 1024 * 1024;

        public ResourceView(IResource resource, string contentType)
        {
            _resource = resource;
            _contentType = contentType;
        }

        public void Render(Http.IResponse response)
        {
        }
    }
}
