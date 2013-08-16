using System;
using Dolstagis.Contrib.Auth.Models;
using NUnit.Framework;

namespace Dolstagis.Tests.Accounts
{
    [TestFixture]
    public class UserSessionFixture
    {
        [Test]
        public void SessionIDIs32CharsLong()
        {
            for (var i = 0; i < 1000; i++) {
                var session = new UserSession(null, null, DateTime.UtcNow);
                Assert.AreEqual(session.SessionID.Length, 32);
            }
        }
    }
}
