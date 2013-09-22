using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dolstagis.Web.Aspnet.Sample
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            For<IRequestProcessor>().Use<SampleRequestProcessor>();
        }
    }
}