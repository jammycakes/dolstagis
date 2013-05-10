using System;
using System.Linq;
using System.Web;
using Dolstagis.Contrib.Auth;
using Dolstagis.Contrib.Auth.Models;
using Dolstagis.Contrib.Auth.Passwords;
using Dolstagis.Contrib.Auth.Passwords.BCrypt;
using Dolstagis.Core.Time;
using Moq;
using Ninject;
using NUnit.Framework;

namespace Dolstagis.Tests.Accounts
{
    [TestFixture]
    public class UserManagerFixture : NHibernateFixtureBase
    {
        private UserManager userManager;
        private IClock clock;
        private DateTime time = DateTime.UtcNow;

        private User JeremyClarkson = new User() {
            UserName = "JeremyClarkson",
            EmailAddress = "jeremy.clarkson@topgear.com",
            DisplayName = "Jeremy Clarkson",
            PasswordHash = "$pt$password",
            IsSuperUser = true
        };

        private User RichardHammond = new User() {
            UserName = "thehamsterscage",
            EmailAddress = "richard.hammond@topgear.com",
            DisplayName = "Richard Hammond",
            IsSuperUser = false
        };

        private User JamesMay = new User() {
            UserName = "MrJamesMay",
            EmailAddress = "james.may@topgear.com",
            DisplayName = "James May",
            IsSuperUser = false
        };

        private User TheStig = new User() {
            UserName = "TheStig",
            EmailAddress = "the.stig@topgear.com",
            DisplayName = "The Stig",
            IsSuperUser = false
        };

        protected override void BeforeFixture()
        {
            this.Kernel.Load(new AuthModule());

            var mClock = new Mock<IClock>();
            mClock.Setup(x => x.Now()).Returns(time.ToLocalTime());
            mClock.Setup(x => x.UtcNow()).Returns(time);
            clock = mClock.Object;
            this.Kernel.Rebind<IClock>().ToConstant(clock);

            userManager = Kernel.Get<UserManager>();
            this.Session.Save(JeremyClarkson);
            this.Session.Save(RichardHammond);
            this.Session.Save(JamesMay);
            this.Session.Save(TheStig);
            this.Session.Flush();
            this.Session.Clear();
        }

        private HttpRequestBase GetMockRequest()
        {
            var mock = new Mock<HttpRequestBase>();
            mock.SetupGet(x => x.UserAgent).Returns("Mozilla/5.0 MSIE");
            return mock.Object;
        }

        [Test]
        public void CanFetchUserByName()
        {
            var user = userManager.GetUserByUserName(RichardHammond.UserName.ToUpper()); // verify case insensitivity
            Assert.IsNotNull(user);
            Assert.AreEqual(RichardHammond.DisplayName, user.DisplayName);
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
            var user = userManager.GetUsersByUserNameOrEmail(RichardHammond.EmailAddress).Single();
            Assert.AreEqual(RichardHammond.DisplayName, user.DisplayName);
        }

        [Test]
        public void CanLoginAndAutomaticallyUpgradesPassword()
        {
            var session = userManager.Login(JeremyClarkson.UserName, "password", GetMockRequest());
            Assert.AreEqual(JeremyClarkson.DisplayName, session.User.DisplayName);
            Assert.False(session.User.PasswordHash.Contains("password"));

            var bcrypt = this.Kernel.Get<BCryptPasswordProvider>();
            var result = bcrypt.Verify("password", session.User.PasswordHash);
            Assert.AreEqual(PasswordResult.Correct, result);
        }


        [Test]
        public void CanCreateAndFetchUserToken()
        {
            var tokens = userManager.CreateTokens(RichardHammond.UserName, "resetpassword");
            Assert.AreEqual(1, tokens.Count());
            Assert.AreEqual(RichardHammond.DisplayName, tokens.First().User.DisplayName);
            this.Session.Clear();
            var token = userManager.GetToken(tokens.First().Token);
            Assert.IsNotNull(token);
            Assert.AreEqual(RichardHammond.DisplayName, tokens.First().User.DisplayName);
        }

        [Test]
        public void DeletingUserTokenDoesNotDeleteUser()
        {
            var tokens = userManager.CreateTokens(RichardHammond.UserName, "resetpassword");
            Assert.AreEqual(1, tokens.Count());
            Assert.AreEqual(RichardHammond.DisplayName, tokens.First().User.DisplayName);
            this.Session.Clear();
            userManager.DeleteToken(tokens.First());
            this.Session.Flush();
            this.Session.Clear();
            var token = userManager.GetToken(tokens.First().Token);
            Assert.IsNull(token, "Token has not been deleted.");
            var user = userManager.GetUserByUserName(tokens.First().User.UserName);
            Assert.IsNotNull(user, "User has been deleted.");
        }

        [Test]
        public void CanGetSessionsFromUser()
        {
            var userSession = userManager.Login(JeremyClarkson.UserName, "password", GetMockRequest());
            this.Session.Flush();
            this.Session.Clear();
            var userSession2 = this.Session.Get<UserSession>(userSession.SessionID);
            var user2 = userSession2.User;
            CollectionAssert.Contains(userSession2.User.Sessions, userSession2);
        }
    }
}
