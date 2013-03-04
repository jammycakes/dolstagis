using Dolstagis.Accounts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
