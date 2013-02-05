using Dolstagis.Accounts;
using Dolstagis.Accounts.Passwords;
using Dolstagis.Accounts.Passwords.BCrypt;
using Dolstagis.Core.IO;
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
            this.Kernel.Load(new AccountsNinjectModule());

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
                PasswordHash = "$pt$password",
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
        public void CanLoginAndAutomaticallyUpgradesPassword()
        {
            var user = userManager.Login("JeremyClarkson", "password");
            Assert.AreEqual("Jeremy Clarkson", user.DisplayName);
            Assert.False(user.PasswordHash.Contains("password"));

            var bcrypt = this.Kernel.Get<BCryptPasswordProvider>();
            var result = bcrypt.Verify("password", user.PasswordHash);
            Assert.AreEqual(PasswordResult.Correct, result);
        }


        [Test]
        public void CanCreateAndFetchUserToken()
        {
            var tokens = userManager.CreateTokens("thehamsterscage", "resetpassword");
            Assert.AreEqual(1, tokens.Count());
            Assert.AreEqual("Richard Hammond", tokens.First().User.DisplayName);
            this.Session.Clear();
            var token = userManager.GetToken(tokens.First().Token);
            Assert.IsNotNull(token);
            Assert.AreEqual("Richard Hammond", tokens.First().User.DisplayName);
        }

        [Test]
        public void DeletingUserTokenDoesNotDeleteUser()
        {
            var tokens = userManager.CreateTokens("thehamsterscage", "resetpassword");
            Assert.AreEqual(1, tokens.Count());
            Assert.AreEqual("Richard Hammond", tokens.First().User.DisplayName);
            this.Session.Clear();
            userManager.DeleteToken(tokens.First());
            this.Session.Flush();
            this.Session.Clear();
            var token = userManager.GetToken(tokens.First().Token);
            Assert.IsNull(token, "Token has not been deleted.");
            var user = userManager.GetUserByUserName(tokens.First().User.UserName);
            Assert.IsNotNull(user, "User has been deleted.");
        }
    }
}
