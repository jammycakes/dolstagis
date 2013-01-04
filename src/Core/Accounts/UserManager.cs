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
    }
}
