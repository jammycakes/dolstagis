using Dolstagis.Contrib.Auth.Passwords;
using Dolstagis.Core;
using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using Dolstagis.Core.Time;
using Dolstagis.Core.WebInfo;
using NHibernate;
using NHibernate.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using NHibernate.Type;

namespace Dolstagis.Contrib.Auth
{
    public class UserManager : ManagerBase
    {
        [Inject, Optional]
        public IMailer mailer { get; set; }

        [Inject, Optional]
        public ITemplateEngine templateEngine { get; set; }

        [Inject]
        public IAccountSettings Settings { get; set; }

        [Inject]
        public IPasswordProvider PasswordProvider { get; set; }

        [Inject]
        public WebInfoManager WebInfo { get; set; }

        public UserManager(ISessionFactory sessionFactory) : base(sessionFactory) { }

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
        ///  Gets a user by login credentials.
        /// </summary>
        /// <param name="username">
        ///  The name of the user to get.
        /// </param>
        /// <param name="password">
        ///  The password of the user to get.
        /// </param>
        /// <param name="request">
        ///  The <see cref="HttpRequestBase"/> instance encapsulating this login request.
        /// </param>
        /// <returns>
        ///  A <see cref="User"/> object, or null if the user was not found.
        /// </returns>

        public UserSession Login(string username, string password, HttpRequestBase request)
        {
            var user = GetUserByUserName(username);
            if (user == null) return null;
            var isValid = this.PasswordProvider.Verify(password, user.PasswordHash);
            switch (isValid) {
                case PasswordResult.CorrectButInsecure:
                    user.PasswordHash = this.PasswordProvider.ComputeHash(password);
                    goto case PasswordResult.Correct;
                case PasswordResult.Correct:
                    return CreateSessionFor(user, request.UserAgent, request.UserHostAddress);
                default:
                    return null;
            }
        }

        /// <summary>
        ///  Create a new session for the given user.
        /// </summary>
        /// <param name="user">
        ///  The user for whom we are creating a new login session.
        /// </param>
        /// <param name="sUserAgent">
        ///  The user agent string.
        /// </param>
        /// <param name="ipAddress">
        ///  The user's client IP address.
        /// </param>
        /// <returns>
        ///  A new <see cref="UserSession"/> instance.
        /// </returns>

        public UserSession CreateSessionFor(User user, string sUserAgent, string ipAddress)
        {
            var userAgent = WebInfo.GetUserAgent(sUserAgent);
            var result = new UserSession(user, userAgent, Clock.UtcNow());
            result.IPAddress = ipAddress;
            this.Session.Save(result);
            this.Session.Flush();
            return result;
        }


        /// <summary>
        ///  Accesses a user session (ie gets it and updates the last access time)
        /// </summary>
        /// <param name="sessionID">
        ///  The session ID.
        /// </param>
        /// <returns>
        ///  The <see cref="UserSession"/> instance, or null if there isn't one.
        /// </returns>

        public UserSession AccessSession(string sessionID)
        {
            var session = this.Session.Get<UserSession>(sessionID);
            if (session != null) {
                session.DateLastAccessed = this.Clock.UtcNow();
                this.Session.Flush();
            }
            return session;
        }

        /// <summary>
        ///  Deletes a user session.
        /// </summary>
        /// <param name="userSession">
        ///  The session to delete.
        /// </param>

        public void DeleteSession(UserSession userSession)
        {
            if (userSession != null) {
                this.Session.Delete(userSession);
                this.Session.Flush();
            }
        }

        /// <summary>
        ///  Deletes a user session by ID.
        /// </summary>
        /// <param name="sessionID">
        ///  The session ID.
        /// </param>
        /// <returns>
        ///  The session which has been deleted.
        /// </returns>

        public UserSession DeleteSession(string sessionID)
        {
            var userSession = this.Session.Get<UserSession>(sessionID);
            DeleteSession(userSession);
            return userSession;
        }

        /// <summary>
        ///  Delete all other sessions for a user.
        /// </summary>
        /// <param name="currentSession">
        ///  The user's current session.
        /// </param>

        public void DeleteOtherSessions(UserSession currentSession)
        {
            if (currentSession == null) return;
            this.Session.Delete(
                "from UserSession where SessionID != ? and User = ?",
                new object[] { currentSession.SessionID, currentSession.User },
                new IType[] { NHibernateUtil.String, NHibernateUtil.Entity(typeof(User)) }
            );
        }

        /// <summary>
        ///  Gets all a user's sessions with a single database query.
        /// </summary>
        /// <param name="user">
        ///  The user whose sessions we are to list.
        /// </param>
        /// <returns>
        ///  A list of user sessions.
        /// </returns>
        /// <remarks>
        ///  This eliminates a select n+1 problem when listing a user's login sessions.
        ///  User agent information will also be retrieved.
        /// </remarks>

        public IEnumerable<UserSession> GetSessionsForUser(User user)
        {
            return this.Session.Query<UserSession>().Fetch(x => x.UserAgent)
                .Where(s => s.User == user)
                .OrderByDescending(s => s.DateLastAccessed);
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
            var expires = Clock.UtcNow().Add(Settings.TokenLifetime);
            var users = GetUsersByUserNameOrEmail(username);
            var tokens = users.Select(x => new UserToken(x, action, expires))
                .ToList();  // We need this to freeze the UserToken instances.
            foreach (var token in tokens)
                this.Session.Persist(token);
            this.Session.Flush();
            return tokens;
        }

        /// <summary>
        ///  Fetches a user token by token ID.
        /// </summary>
        /// <param name="tokenID">
        ///  The token GUID.
        /// </param>
        /// <returns>
        ///  A <see cref="UserToken"/> instance, or null if none present.
        /// </returns>

        public UserToken GetToken(Guid tokenID)
        {
            var token = this.Session.Get<UserToken>(tokenID);
            if (token == null) {
                return null;
            }
            else if (token.IsValid(this.Clock.UtcNow())) {
                return token;
            }
            else {
                DeleteToken(token);
                return null;
            }
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

        /// <summary>
        ///  Attempts to change a user's password.
        /// </summary>
        /// <param name="user">
        ///  The user whose password we are changing.
        /// </param>
        /// <param name="oldPassword">
        ///  The old password.
        /// </param>
        /// <param name="newPassword">
        ///  The new password.
        /// </param>

        public void ChangePassword(User user, string oldPassword, string newPassword)
        {
            var verification = this.PasswordProvider.Verify(oldPassword, user.PasswordHash);
            if (verification != PasswordResult.Correct && verification != PasswordResult.CorrectButInsecure) {
                throw new UserException("Your password was not correct.");
            }

            this.SetPassword(user, newPassword);
        }

        /// <summary>
        ///  Sets a user's password, without checking the old one first.
        /// </summary>
        /// <param name="user">The user whose password we are changing.</param>
        /// <param name="password">The new password.</param>

        public void SetPassword(User user, string password)
        {
            user.PasswordHash = this.PasswordProvider.ComputeHash(password);
        }
    }
}
