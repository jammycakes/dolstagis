using Dolstagis.Accounts.Passwords;
using Dolstagis.Accounts.Passwords.BCrypt;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
    }
}
