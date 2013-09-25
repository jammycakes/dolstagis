using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
using Dolstagis.Web.Http;
using Moq;
using NUnit.Framework;

namespace Dolstagis.Tests.Web
{
    [TestFixture]
    public class ApplicationFixture
    {
        public interface IDisposableRequestProcessor: IRequestProcessor, IDisposable
        {}

        private class TestModule : Module
        {
            public TestModule(Func<IRequestProcessor> getter)
            {
                Services.For<IRequestProcessor>().Use(getter);
            }
        }

        [Test]
        public void VerifyRequestProcessorIsDisposedAfterRequestHasFinished()
        {
            var mockProcessor = new Mock<IDisposableRequestProcessor>();
            var module = new TestModule(() => mockProcessor.Object);
            var application = new Application().AddModules(module);
            var context = new Mock<IRequestContext>();

            application.ProcessRequest(context.Object);

            mockProcessor.Verify(x => x.Process(It.IsAny<IRequestContext>()));
            mockProcessor.Verify(x => x.Dispose());
        }

        [Test]
        public void CanGetModules()
        {
            var module = new Module();
            var application = new Application().AddModules(module);

            var modules = application.GetModules().ToList();

            Assert.AreSame(module, modules.Single());
        }
    }
}
