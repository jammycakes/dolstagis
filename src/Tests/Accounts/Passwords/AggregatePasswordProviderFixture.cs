using Dolstagis.Accounts.Passwords;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Tests.Accounts.Passwords
{
    [TestFixture]
    public class AggregatePasswordProviderFixture
    {
        private PasswordResult GetAggregateResults(PasswordResult first, PasswordResult second)
        {
            Mock<IPasswordProvider> firstProvider = new Mock<IPasswordProvider>();
            firstProvider.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(first);
            Mock<IPasswordProvider> secondProvider = new Mock<IPasswordProvider>();
            secondProvider.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(second);
            var aggregate = new AggregatePasswordProvider(new IPasswordProvider[] {
                firstProvider.Object, secondProvider.Object
            });
            return aggregate.Verify(null, null);
        }

        [Test]
        public void ShouldReturnFirstResultWhenFirstProviderCanVerify()
        {
            foreach (PasswordResult r1 in Enum.GetValues(typeof(PasswordResult))) {
                if (r1 != PasswordResult.Unrecognised) {
                    foreach (PasswordResult r2 in Enum.GetValues(typeof(PasswordResult))) {
                        var result = GetAggregateResults(r1, r2);
                        Assert.AreEqual(result, r1);
                    }
                }
            }
        }

        [TestCase(PasswordResult.Correct)]
        [TestCase(PasswordResult.CorrectButInsecure)]
        public void ShouldReturnInsecureWhenFirstProviderCannotVerify(PasswordResult second)
        {
            var first = PasswordResult.Unrecognised;
            var result = GetAggregateResults(first, second);
            Assert.AreEqual(PasswordResult.CorrectButInsecure, result);
        }

        [TestCase(PasswordResult.Incorrect)]
        [TestCase(PasswordResult.Unrecognised)]
        public void ShouldFailWhenNeitherProviderCanVerify(PasswordResult second)
        {
            var first = PasswordResult.Unrecognised;
            var result = GetAggregateResults(first, second);
            Assert.AreEqual(second, result);
        }
    }
}
