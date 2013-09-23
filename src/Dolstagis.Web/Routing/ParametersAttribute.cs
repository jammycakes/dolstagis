using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Routing
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ParametersAttribute : Attribute
    {
        public string Path { get; private set; }

        public IList<string> Components { get; private set; }

        public ParametersAttribute(string path)
        {
            this.Path = path;
            this.Components = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList().AsReadOnly();
        }
    }
}
