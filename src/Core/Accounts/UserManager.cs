using Dolstagis.Core;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    public class UserManager : ManagerBase
    {
        public UserManager(ISessionFactory sessionFactory) : base(sessionFactory) { }

        public UserManager(ISessionFactory sessionFactory, LazyDisposable<ISession> lazySession)
            : base(sessionFactory, lazySession)
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
        ///  Sends a password reset message to all accounts registered with this user name or
        ///  email address.
        /// </summary>
        /// <param name="name">The user name or email address.</param>
        /// <returns>
        ///  The number of users to whom a password reset email has been successfully sent.
        /// </returns>

        public int RequestPasswordReset(string name)
        {
            var users = GetUsersByUserNameOrEmail(name);
            return users.Count();
        }
    }
}
