using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dolstagis.Tests.Core.Module;
using Dolstagis.Tests.Core.Module.Mappings;
using NUnit.Framework;

namespace Dolstagis.Tests.Core
{
    [TestFixture]
    public class ModuleFixture
    {
        [Test]
        public void CanEnumerateNHibernateMappings()
        {
            var module = new TestModule();
            var mappings = module.GetNHibernateMappings();
            CollectionAssert.AreEqual(mappings, new Type[] { typeof(DummyMap) });
        }
    }
}
