using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dolstagis.Contrib.Auth.Models;
using Dolstagis.Core.Data;
using NHibernate;
using NHibernate.Type;

namespace Dolstagis.Contrib.Auth
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        { }

        public UserRepository(ISessionFactory sessionFactory, ISession session)
            : base(sessionFactory, session)
        { }


        public void DeleteOtherSessionsForUser(string sessionID, User user)
        {
            this.Session.Delete(
                "from UserSession where SessionID != ? and User = ?",
                new object[] { sessionID, user },
                new IType[] { NHibernateUtil.String, NHibernateUtil.Entity(typeof(User)) }
            );
        }
    }
}
