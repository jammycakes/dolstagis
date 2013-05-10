using Dolstagis.Contrib.Auth.Passwords;
using Dolstagis.Contrib.Auth.Passwords.Sha512;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Tests.Accounts.Passwords.Sha512
{
    [TestFixture]
    public class Sha512PasswordProviderFixture
    {
        [Test]
        public void CanHashAndVerifyPassword()
        {
            string password = "Passw0rD1";
            var provider = new Sha512PasswordProvider();
            var hash = provider.ComputeHash(password);
            var testResult = provider.Verify(password, hash);
            Assert.AreEqual(PasswordResult.Correct, testResult);
        }

        [Test]
        public void CanErrorOnIncorrectPassword()
        {
            string password = "Passw0rD1";
            string wrongPassword = "password";
            var provider = new Sha512PasswordProvider();
            var hash = provider.ComputeHash(password);
            var testResult = provider.Verify(wrongPassword, hash);
            Assert.AreEqual(PasswordResult.Incorrect, testResult);
        }
    }
}
