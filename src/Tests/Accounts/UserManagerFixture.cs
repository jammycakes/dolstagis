using Dolstagis.Accounts;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Tests.Accounts
{
    [TestFixture]
    public class UserManagerFixture : NHibernateFixtureBase
    {
        [Test]
        public void CanCreateUser()
        {
            using (var um = Kernel.Get<UserManager>()) {
                var user = new User() {
                    UserName = "jeremy.clarkson",
                    EmailAddress = "theorangutan@topgear.com",
                    DisplayName = "Jeremy Clarkson",
                    IsSuperUser = true
                };
                this.Session.Save(user);
                this.Session.Clear();
                var user1 = um.GetAllUsers().First();
                Assert.AreEqual(user.UserName, user1.UserName);
            }
        }
    }
}
