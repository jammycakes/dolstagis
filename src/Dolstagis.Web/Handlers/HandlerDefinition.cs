using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Handlers
{
    public class HandlerDefinition
    {
        public Type Type { get; private set; }

        public HandlerDefinition(Type type)
        {
            this.Type = type;
        }
    }
}
