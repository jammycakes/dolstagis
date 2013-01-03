using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    public class UserManager
    {
        public ISession Session { get; private set; }

        public UserManager(ISession session)
        {
            this.Session = session;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.Session.Query<User>();
        }
    }
}
