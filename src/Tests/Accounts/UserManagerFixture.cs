using Dolstagis.Accounts;
using Dolstagis.Core.Time;
using Moq;
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
        private UserManager userManager;
        private IClock clock;
        private DateTime time = DateTime.UtcNow;

        protected override void BeforeFixture()
        {
            var mClock = new Mock<IClock>();
            mClock.Setup(x => x.Now()).Returns(time.ToLocalTime());
            mClock.Setup(x => x.UtcNow()).Returns(time);
            clock = mClock.Object;
            this.Kernel.Rebind<IClock>().ToConstant(clock);

            userManager = Kernel.Get<UserManager>();
            this.Session.Save(new User() {
                UserName = "JeremyClarkson",
                EmailAddress = "jeremy.clarkson@topgear.com",
                DisplayName = "Jeremy Clarkson",
                IsSuperUser = true
            });
            this.Session.Save(new User() {
                UserName = "thehamsterscage",
                EmailAddress = "richard.hammond@topgear.com",
                DisplayName = "Richard Hammond",
                IsSuperUser = false
            });
            this.Session.Save(new User() {
                UserName = "MrJamesMay",
                EmailAddress = "james.may@topgear.com",
                DisplayName = "James May",
                IsSuperUser = false
            });
            this.Session.Save(new User() {
                UserName = "TheStig",
                EmailAddress = "the.stig@topgear.com",
                DisplayName = "The Stig",
                IsSuperUser = false
            });
        }

        [Test]
        public void CanFetchUserByName()
        {
            var user = userManager.GetUserByUserName("TheHamstersCage"); // verify case insensitivity
            Assert.IsNotNull(user);
            Assert.AreEqual("Richard Hammond", user.DisplayName);
        }

        [Test]
        public void CanNotFetchNonexistentUser()
        {
            var user = userManager.GetUserByUserName("TheOldStig");
            Assert.IsNull(user);
        }

        [Test]
        public void CanFetchUserByEmail()
        {
            var user = userManager.GetUsersByUserNameOrEmail("richard.hammond@topgear.com").Single();
            Assert.AreEqual("Richard Hammond", user.DisplayName);
        }

        [Test]
        public void CanCreateUserToken()
        {
            var tokens = userManager.CreateTokens("thehamsterscage", "resetpassword");
            Assert.AreEqual(1, tokens.Count());
            Assert.AreEqual("Richard Hammond", tokens.First().User.DisplayName);
            this.Session.Clear();
            var token = this.Session.Get<UserToken>(tokens.First().Token);
            Assert.IsNotNull(token);
            Assert.AreEqual("Richard Hammond", tokens.First().User.DisplayName);
        }
    }
}
