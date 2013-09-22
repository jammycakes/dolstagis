using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dolstagis.Web.Aspnet
{
    public class AspnetRequestContext : IRequestContext
    {
        private HttpContextBase context;

        public AspnetRequestContext(HttpContextBase context)
        {
            this.context = context;
        }

        public IRequest Request { get; private set; }

        public IResponse Response { get; private set; }
    }
}
