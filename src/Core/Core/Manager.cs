using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core
{
    public class Manager<T>: ManagerBase where T: new()
    {
        public Manager(ISessionFactory sessionFactory)
            : base(sessionFactory)
        { }

        public Manager(ISessionFactory sessionFactory, ISession session)
            : base(sessionFactory, session)
        { }

        public T Get(object id)
        {
            return this.Session.Get<T>(id);
        }

        public T Create()
        {
            var result = new T();
            this.Session.Persist(result);
            return result;
        }

        public void SaveNow(T obj)
        {
            this.Session.SaveOrUpdate(obj);
            this.Session.Flush();
        }

        public void DeleteNow(T obj)
        {
            this.Session.Delete(obj);
            this.Session.Flush();
        }

        public IQueryable<T> Query()
        {
            return this.Session.Query<T>();
        }
    }
}
