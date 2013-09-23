using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
using Dolstagis.Web.Handlers;
using NUnit.Framework;

namespace Dolstagis.Tests.Web.Handlers
{
    [TestFixture]
    public class HandlerRegistryFixture
    {
        [Test]
        public void CanCreateHandlerRegistry()
        {
            using (var container = new StructureMap.Container()) {
                container.Configure(x => {
                    x.For<Module>().Add(new Module());
                    x.For<Module>().Add(new Module());
                });

                var registry = container.GetInstance<HandlerRegistry>();

                Assert.AreEqual(2, registry.Modules.Count());
            }
        }
    }
}
