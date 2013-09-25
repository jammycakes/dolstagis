using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
using Dolstagis.Web.Http;
using Moq;
using NUnit.Framework;
using StructureMap.Configuration.DSL;

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


        private class IOCModule : Module
        {
            public void Configure(Action<Registry> configureAction)
            {
                configureAction(this.Services);
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

        [Test]
        public void VerifyRequestIsOnlyProcessedOnce()
        {
            var mockActionLocator = new Mock<IActionLocator>();
            var mockContext = new Mock<IRequestContext>();
            mockContext.SetupGet(x => x.Request).Returns(Mock.Of<IRequest>());
            mockContext.SetupGet(x => x.Response).Returns(Mock.Of<IResponse>());

            var module = new IOCModule();
            module.Configure(x => {
                x.For<IActionLocator>().Use(mockActionLocator.Object);
            });

            var application = new Application();
            application.AddModules(module);
            try {
                application.ProcessRequest(mockContext.Object);
            }
            catch {
                // This will eventually throw an HTTP exception because there's
                // nothing to see. We're not interested in that.
            }

            mockActionLocator.Verify(x => x.GetAction(It.IsAny<IRequest>()), Times.Once());
        }
    }
}
