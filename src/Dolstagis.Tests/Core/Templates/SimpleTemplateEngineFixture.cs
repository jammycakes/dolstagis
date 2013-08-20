using Dolstagis.Framework.IO;
using Dolstagis.Framework.Templates;
using Moq;
using NUnit.Framework;

namespace Dolstagis.Tests.Core.Templates
{
    [TestFixture]
    public class SimpleTemplateEngineFixture
    {
        [Test]
        public void CanParseTemplate()
        {
            var filespace = new Mock<IFilespace>();
            filespace.Setup(x => x.Read(It.IsAny<string>())).Returns
                ("1:{{one}} 2:{{two}} 3:{{three}} 4:{{four}}");
            var templateEngine = new SimpleTemplateEngine(filespace.Object);

            var result = templateEngine.Process("anything", new { one="1", two=2, three=(string)null });
            Assert.AreEqual("1:1 2:2 3: 4:{{four}}", result);
        }
    }
}
