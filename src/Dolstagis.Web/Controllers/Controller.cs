using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Controllers
{
    public class Controller
    {
        public Type Type { get; private set; }

        public Controller(Type type)
        {
            this.Type = type;
        }
    }
}
