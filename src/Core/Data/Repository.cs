using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dolstagis.Core.Data
{
    public class Repository<TModel> : IDisposable where TModel: new()
    {
        private bool _ownsSession = false;

        protected ISessionFactory SessionFactory { get; private set; }

        public ISession Session { get; private set; }

        public Repository(ISessionFactory sessionFactory)
        {
            this.SessionFactory = sessionFactory;
            this.Session = SessionFactory.OpenSession();
            this._ownsSession = true;
        }

        public Repository(ISessionFactory sessionFactory, ISession session)
        {
            this.SessionFactory = sessionFactory;
            this.Session = session;
            this._ownsSession = false;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~Repository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this._ownsSession) {
                this.Session.Flush();
                this.Session.Dispose();
            }
        }
    }
}
