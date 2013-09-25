using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dolstagis.Web.Aspnet.Sample
{
    public class Module : Dolstagis.Web.Module
    {
        public Module()
        {
            AddHandler<HomeHandler>();
        }
    }
}