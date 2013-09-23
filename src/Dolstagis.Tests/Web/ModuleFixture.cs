using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
using NUnit.Framework;

namespace Dolstagis.Tests.Web
{
    [TestFixture]
    public class ModuleFixture
    {
        [Test]
        public void CanAddController()
        {
            var module = new Module();
            module.AddHandler<object>();
            Assert.AreEqual(1, module.Handlers.Count);
            Assert.AreEqual(typeof(object), module.Handlers[0].Type);
        }
    }
}
