using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Dolstagis.Web.Static;

namespace Dolstagis.Web.Aspnet.Sample
{
    public class Module : Dolstagis.Web.Module
    {
        public Module()
        {
            AddHandler<HomeHandler>();
            AddHandler<StaticFileHandler>("content");
            this.Services.For<IResourceLocator>().Singleton().Use
                (new FileResourceLocator(HostingEnvironment.MapPath("~/content")));
        }
    }
}