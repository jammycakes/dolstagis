using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
using Dolstagis.Tests.Web.Handlers.TestHandlers;
using NUnit.Framework;
using System.Reflection;
using Dolstagis.Web.Handlers;
using Moq;
using Dolstagis.Web.Http;

namespace Dolstagis.Tests.Web
{
    [TestFixture]
    public class ActionLocatorFixture
    {
        private void AssertAction<TAction>(Expression<Func<TAction, object>> expected, IAction actual)
        {
            Assert.AreEqual(typeof(TAction), actual.HandlerType);
            Assert.AreEqual(((MethodCallExpression)expected.Body).Method, actual.Method);
            CollectionAssert.AreEqual(
                ((MethodCallExpression)expected.Body).Arguments.Cast<ConstantExpression>().Select(x => x.Value),
                actual.Parameters
            );
        }


        private IAction GetAction(string method, string path)
        {
            var registry = new HandlerRegistry(new Dolstagis.Web.Module[] { new TestModule() });
            var mockAction = new Mock<IAction>();
            mockAction.SetupAllProperties();
            var locator = new ActionLocator(registry, () => mockAction.Object);
            var request = new Mock<IRequest>();
            request.SetupGet(x => x.AppRelativePath).Returns(path);
            request.SetupGet(x => x.Method).Returns(method);
            return locator.GetAction(request.Object);
        }


        [Test]
        public void CanGetRootAction()
        {
            AssertAction<RootHandler>(x => x.Get(), GetAction("GET", "/"));
            AssertAction<RootHandler>(x => x.Post(""), GetAction("POST", "/"));
            AssertAction<RootHandler>(x => x.Post("wibble"), GetAction("POST", "/wibble"));
            AssertAction<RootHandler>(x => x.Post("wibble/wobble"), GetAction("POST", "/wibble/wobble/"));
        }

        [Test]
        public void CanGetChildAction()
        {
            AssertAction<FirstHandler>(x => x.Get("Arrietty", "Clock"), GetAction("GET", "/group/one/Arrietty/Clock"));
            Assert.IsNull(GetAction("GET", "/group/one/Arrietty/"));
            Assert.IsNull(GetAction("GET", "/group"));
        }
    }
}
