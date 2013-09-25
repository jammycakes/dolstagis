using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Http
{
    public interface IResponse
    {
        void AddHeader(string name, string value);

        Stream ResponseStream { get; }

        int StatusCode { get; set; }

        string StatusDescription { get; set; }
    }
}
