using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Http
{
    public interface IRequest
    {
        /// <summary>
        ///  The path of the application root, including a trailing slash.
        /// </summary>

        string AppRoot { get; }

        /// <summary>
        ///  The path of the requested URL relative to the application root,
        ///  without a leading slash.
        /// </summary>

        string AppRelativePath { get; }

        /// <summary>
        ///  The HTTP request method
        /// </summary>

        string Method { get; }

        /// <summary>
        ///  The complete URI of the request
        /// </summary>

        Uri Url { get; }
    }
}
