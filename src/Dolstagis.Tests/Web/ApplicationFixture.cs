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

        private class TestRegistry : Registry
        {
            public TestRegistry(Func<IRequestProcessor> getter)
            {
                For<IRequestProcessor>().Use(getter);
            }
        }

        private class TestApplication : Application
        {
            private Registry registry;

            public TestApplication(Registry registry)
            {
                this.registry = registry;
            }

            public override IEnumerable<Registry> GetRegistries()
            {
                yield return this.registry;
            }
        }

        [Test]
        public void VerifyRequestProcessorIsDisposedAfterRequestHasFinished()
        {
            var mockProcessor = new Mock<IDisposableRequestProcessor>();
            var registry = new TestRegistry(() => mockProcessor.Object);
            var application = new TestApplication(registry);
            var context = new Mock<IRequestContext>();

            application.ProcessRequest(context.Object);

            mockProcessor.Verify(x => x.Process(It.IsAny<IRequestContext>()));
            mockProcessor.Verify(x => x.Dispose());
        }
    }
}
