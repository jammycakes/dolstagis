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
            module.AddController<object>();
            Assert.AreEqual(1, module.Controllers.Count);
            Assert.AreEqual(typeof(object), module.Controllers[0].Type);
        }
    }
}
