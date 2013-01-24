using Dolstagis.Core;
using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using Dolstagis.Core.Time;
using NHibernate;
using NHibernate.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    public class UserManager : ManagerBase
    {
        [Inject]
        public IClock Clock { get; set; }

        [Inject, Optional]
        public IMailer mailer { get; set; }

        [Inject, Optional]
        public ITemplateEngine templateEngine { get; set; }

        public UserManager(ISessionFactory sessionFactory) : base(sessionFactory) { }

        public UserManager(ISessionFactory sessionFactory, LazyDisposable<ISession> lazySession)
            : base(sessionFactory, lazySession)
        { }

        public UserManager(ISessionFactory sessionFactory, ISession session)
            : base(sessionFactory, session)
        { }

        public IEnumerable<User> GetAllUsers()
        {
            return this.Session.Query<User>();
        }

        /// <summary>
        ///  Gets the <see cref="User"/> object for a given username.
        /// </summary>
        /// <param name="username">The user name.</param>
        /// <returns>
        ///  The <see cref="User"/> object, or null if no user has been found with
        ///  that name in the database.
        /// </returns>

        public User GetUserByUserName(string username)
        {
            username = username.ToLower();
            return this.Session.Query<User>()
                .Where(x => x.UserName.ToLower() == username)
                .SingleOrDefault();
        }

        /// <summary>
        ///  Gets all <see cref="User"/> objects for a given username or email address.
        /// </summary>
        /// <param name="value">The user name or email address.</param>
        /// <returns>
        ///  All users with the given username or email address.
        /// </returns>

        public IEnumerable<User> GetUsersByUserNameOrEmail(string value)
        {
            value = value.ToLower();
            return this.Session.Query<User>()
                .Where(x => x.UserName.ToLower() == value || x.EmailAddress.ToLower() == value);
        }

        /// <summary>
        ///  Creates user tokens for all users with a given username or email address.
        /// </summary>
        /// <param name="username">
        ///  The name of the user to create.
        /// </param>
        /// <param name="action">
        ///  The action which this token is to execute.
        /// </param>
        /// <returns>
        ///  A list of <see cref="UserToken"/> instances.
        /// </returns>

        public IEnumerable<UserToken> CreateTokens(string username, string action)
        {
            var users = GetUsersByUserNameOrEmail(username);
            var tokens = users.Select(x => new UserToken(x, action, this.Clock))
                .ToList();  // We need this to freeze the UserToken instances.
            foreach (var token in tokens)
                this.Session.Persist(token);
            this.Session.Flush();
            return tokens;
        }

        /// <summary>
        ///  Fetches a user token by token ID.
        /// </summary>
        /// <param name="token">
        ///  The token GUID.
        /// </param>
        /// <returns>
        ///  A <see cref="UserToken"/> instance, or null if none present.
        /// </returns>

        public UserToken GetToken(Guid token)
        {
            return this.Session.Get<UserToken>(token);
        }

        /// <summary>
        ///  Deletes a user token.
        /// </summary>
        /// <param name="token">
        ///  The token to be deleted.
        /// </param>

        public void DeleteToken(UserToken token)
        {
            this.Session.Delete(token);
        }

        /// <summary>
        ///  Sends a password reset message to all accounts registered with this user name or
        ///  email address.
        /// </summary>
        /// <param name="name">The user name or email address.</param>
        /// <returns>
        ///  The number of users to whom a password reset email has been successfully sent.
        /// </returns>

        public int RequestPasswordReset(string name)
        {
            var tokens = CreateTokens(name, "ResetPassword");
            foreach (var token in tokens) {
                var message = new Message() {
                    Html = this.templateEngine.Process("Accounts/ResetPassword.html", token),
                    Text = this.templateEngine.Process("Accounts/ResetPassword.txt", token),
                    Subject = "Reset your password"
                };
                this.mailer.Send(token.User, message);
            }
            return tokens.Count();
        }
    }
}
