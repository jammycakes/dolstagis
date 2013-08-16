using Dolstagis.Contrib.Auth.Passwords;
using Dolstagis.Contrib.Auth.Passwords.BCrypt;
using Dolstagis.Contrib.Auth.Passwords.Sha512;
using Moq;
using NUnit.Framework;


namespace Dolstagis.Tests.Accounts.Passwords.BCrypt
{
    [TestFixture]
    public class BCryptPasswordProviderFixture
    {
        private BCryptPasswordProvider GetProvider(int workFactor)
        {
            var settings = new Mock<IBCryptSettings>();
            settings.Setup(x => x.WorkFactor).Returns(workFactor);
            return new BCryptPasswordProvider(settings.Object);
        }

        [Test]
        public void CanHashAndVerifyPassword()
        {
            string password = "Passw0rD1";
            int workFactor = 5;

            var provider = GetProvider(workFactor);
            var hash = provider.ComputeHash(password);
            var testResult = provider.Verify(password, hash);
            Assert.AreEqual(PasswordResult.Correct, testResult);
        }

        [Test]
        public void CanHashAndVerifyInsecurePassword()
        {
            string password = "Passw0rd1";
            int oldWorkFactor = 5;
            int newWorkFactor = 10;

            var hash = GetProvider(oldWorkFactor).ComputeHash(password);
            var testResult = GetProvider(newWorkFactor).Verify(password, hash);
            Assert.AreEqual(PasswordResult.CorrectButInsecure, testResult);
        }

        [Test]
        public void IgnoresPasswordFromOtherProvider()
        {
            string password = "Passw0rd1";
            var provider = GetProvider(8);
            var otherProvider = new Sha512PasswordProvider();
            var hash = otherProvider.ComputeHash(password);
            var testResult = provider.Verify(password, hash);
            Assert.AreEqual(PasswordResult.Unrecognised, testResult);
        }
    }
}
