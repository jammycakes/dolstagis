using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web;
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
                For<IRequestProcessor>().Use(getter);
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
    }
}
