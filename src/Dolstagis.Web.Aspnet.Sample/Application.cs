using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dolstagis.Web;

namespace Dolstagis.Web.Aspnet.Sample
{
    public class Application : Dolstagis.Web.Application
    {
        public override IEnumerable<StructureMap.Configuration.DSL.Registry> GetRegistries()
        {
            yield return new Registry();
        }
    }
}