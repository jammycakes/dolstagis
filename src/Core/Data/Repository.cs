using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;

namespace Dolstagis.Core.Data
{
    /* ====== Repository class ====== */

    /// <summary>
    ///  This is the base class for our data access queries. It serves three purposes:
    ///  (a) to make Linq to NHibernate mockable
    ///  (b) to provide a common lifecycle management mechanism for ISession, whether
    ///      instantiated within an HTTP request or not.
    ///  (c) to allow us to isolate and mock fine tuned database queries and commands
    ///      for which Linq to NHibernate is not sufficient. Anything to do with
    ///      HQL or ICriteria should go in Repository subclasses, as should anything
    ///      involving stored procedures, for instance.
    ///  It is NOT intended to let you swap out NHibernate for Entity Framework.
    ///  That is a terrible idea: we're not going there.
    /// </summary>
    /// <typeparam name="TModel">
    ///  The entity class on which the repository is based.
    /// </typeparam>

    public class Repository<TModel> : IRepository<TModel>, IDisposable
        where TModel: new()
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

        public TModel Get(object id)
        {
            return Session.Get<TModel>(id);
        }

        public IQueryable<TModel> Query()
        {
            return Session.Query<TModel>();
        }

        public virtual TModel CreateTransient()
        {
            return new TModel();
        }

        public TModel CreatePersistent()
        {
            var result = CreateTransient();
            Session.Persist(result);
            return result;
        }

        public void Persist(TModel obj)
        {
            Session.Persist(obj);
        }

        public void Flush()
        {
            Session.Flush();
        }
    }
}
