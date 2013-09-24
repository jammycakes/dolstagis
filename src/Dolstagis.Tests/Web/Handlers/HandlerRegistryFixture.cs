﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
using Dolstagis.Web.Handlers;
using Dolstagis.Web.Routing;
using NUnit.Framework;

namespace Dolstagis.Tests.Web.Handlers
{
    [TestFixture]
    public class HandlerRegistryFixture
    {
        [Test]
        public void CanRegisterRootHandler()
        {
            var registry = new HandlerRegistry(Enumerable.Empty<Module>());
            registry.AddHandler<Root>();
            Assert.IsNotNull(registry.Root);
            Assert.AreEqual(registry.Root.Definition.Type, typeof(Root));
            Assert.AreEqual("/", registry.Root.Path);
        }

        [Test]
        public void CanRegisterHandlerWithPath()
        {
            var registry = new HandlerRegistry(Enumerable.Empty<Module>());
            registry.AddHandler<First>();
            Assert.IsNull(registry.Root.Definition);
            var level1 = registry.Root.GetChild("handlers");
            var level2 = level1.GetChild("first");
            Assert.IsNotNull(level1);
            Assert.IsNull(level1.Definition);
            Assert.AreEqual("/handlers/", level1.Path);
            Assert.IsNotNull(level2);
            Assert.AreEqual(typeof(First), level2.Definition.Type);
            Assert.AreEqual("/handlers/first/", level2.Path);
        }

        [Test]
        public void CanRegisterMultiplePathHandlers()
        {
            var registry = new HandlerRegistry(Enumerable.Empty<Module>());
            registry.AddHandler<First>();
            registry.AddHandler<Second>();
            registry.AddHandler<Root>();
            var level1 = registry.Root.GetChild("handlers");
            var level2First = level1.GetChild("first");
            var level2Second = level1.GetChild("SECOND");

            Assert.AreEqual(typeof(Root), registry.Root.Definition.Type);
            Assert.IsNull(level1.Definition);
            Assert.AreEqual(typeof(First), level2First.Definition.Type);
            Assert.AreEqual(typeof(Second), level2Second.Definition.Type);
        }

        [Test]
        public void CanRegisterHandlerWithExplicitPath()
        {
            var registry = new HandlerRegistry(Enumerable.Empty<Module>());
            registry.AddHandler<Root>("/one/two");
            var level1 = registry.Root.GetChild("one");
            var level2 = level1.GetChild("two");
            Assert.IsNull(registry.Root.Definition);
            Assert.IsNull(level1.Definition);
            Assert.AreEqual(typeof(Root), level2.Definition.Type);
        }

        [Test]
        public void CanRegisterTheSameHandlerWithTwoDifferentPaths()
        {
            var registry = new HandlerRegistry(Enumerable.Empty<Module>());
            registry.AddHandler<Root>("/one/two");
            registry.AddHandler<Root>("/one/three");
            var level1 = registry.Root.GetChild("one");
            var level2a = level1.GetChild("two");
            var level2b = level1.GetChild("three");
            Assert.AreEqual(typeof(Root), level2a.Definition.Type);
            Assert.AreEqual(typeof(Root), level2b.Definition.Type);
            Assert.AreEqual("/one/two", level2a.Definition.Path);
            Assert.AreEqual("/one/three", level2b.Definition.Path);
            Assert.IsFalse(registry.Root.IsValid);
            Assert.IsFalse(level1.IsValid);
            Assert.IsTrue(level2a.IsValid);
            Assert.IsTrue(level2b.IsValid);
        }

        [Test]
        public void CanRegisterHandlersFromModule()
        {
            var module = new TestModule();
            var registry = new HandlerRegistry(new Module[] { module });
            var level1 = registry.Root.GetChild("one");
            var level2a = level1.GetChild("two");
            var level2b = level1.GetChild("three");

            Assert.AreEqual(typeof(Root), registry.Root.Definition.Type);
            Assert.IsNull(level1.Definition);
            Assert.AreEqual(typeof(First), level2a.Definition.Type);
            Assert.AreEqual(typeof(Second), level2b.Definition.Type);
            Assert.AreSame(module, registry.Root.Definition.Module);
            Assert.AreSame(module, level2a.Definition.Module);
            Assert.AreSame(module, level2b.Definition.Module);
        }

        [TestCase("/", typeof(Root), true)]
        [TestCase("/one/two", typeof(First), true)]
        [TestCase("/wibble", null, true)]
        [TestCase("/one", null, true)]
        [TestCase("/one/two/three", null, true)]

        [TestCase("/", typeof(Root), false)]
        [TestCase("/one/two", typeof(First), false)]
        [TestCase("/wibble", typeof(Root), false)]
        [TestCase("/one", typeof(Root), false)]
        [TestCase("/one/two/three", typeof(First), false)]
        public void CanGetHandler(string path, Type expectedType, bool exact)
        {
            var module = new TestModule();
            var registry = new HandlerRegistry(new Module[] { module });

            var registration = registry.GetHandlerRegistration(path, exact);

            if (expectedType == null) {
                Assert.IsNull(registration);
            }
            else {
                Assert.AreEqual(expectedType, registration.Definition.Type);
            }
        }


        [Route("/")]
        private class Root
        {
        }

        [Route("/handlers/first")]
        private class First
        {
        }

        [Route("/handlers/second")]
        private class Second
        {
        }

        private class TestModule : Dolstagis.Web.Module
        {
            public TestModule()
            {
                this.AddHandler<Root>();
                this.AddHandler<First>("/one/two");
                this.AddHandler<Second>("/one/three");
            }
        }
    }
}
