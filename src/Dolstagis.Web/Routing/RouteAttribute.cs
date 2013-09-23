using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Routing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RouteAttribute : Attribute
    {
        public string Path { get; private set; }

        public IList<string> Components { get; private set; }

        public RouteAttribute(string path)
        {
            this.Path = path;
            this.Components = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList().AsReadOnly();
        }
    }
}
