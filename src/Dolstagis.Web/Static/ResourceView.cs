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
            response.AddHeader("Content-Type", _contentType);
            response.AddHeader("Content-Length", _resource.Length.ToString());
            response.AddHeader("Last-Modified", _resource.DateModified.ToString("R"));
            response.AddHeader("Etag", _resource.DateModified.Ticks.ToString("X16"));

            var buffer = new byte[_bufSize];
            using (var stream = _resource.Open()) {
                int bytes;
                while ((bytes = stream.Read(buffer, 0, _bufSize)) > 0) {
                    response.ResponseStream.Write(buffer, 0, bytes);
                }
            }
        }
    }
}
