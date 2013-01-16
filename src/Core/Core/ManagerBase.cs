using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core
{
    public abstract class ManagerBase : IDisposable
    {
        private LazyDisposable<ISession> _session;
        private bool _ownsSession = false;

        protected ISessionFactory SessionFactory { get; private set; }

        protected ISession Session
        {
            get
            {
                return _session.Value;
            }
        }

        /// <summary>
        ///  Creates a new instance of the <see cref="ManagerBase"/> subclass that
        ///  manages its own session lifecycle.
        /// </summary>
        /// <param name="sessionFactory">
        ///  The session factory used to create the session.
        /// </param>

        public ManagerBase(ISessionFactory sessionFactory)
        {
            this.SessionFactory = sessionFactory;
            this._session = new LazyDisposable<ISession>(() => SessionFactory.OpenSession());
            this._ownsSession = true;
        }

        /// <summary>
        ///  Creates a new instance of the <see cref="ManagerBase"/> subclass with an
        ///  injected lazy session whose lifecycle is managed elsewhere.
        /// </summary>
        /// <param name="sessionFactory"></param>
        /// <param name="lazySession"></param>

        public ManagerBase(ISessionFactory sessionFactory, LazyDisposable<ISession> lazySession)
        {
            this.SessionFactory = sessionFactory;
            this._session = lazySession;
            this._ownsSession = false;
        }

        /// <summary>
        ///  Creates a new instance of the <see cref="ManagerBase"/> subclass with an
        ///  injected session whose lifecycle is managed elsewhere.
        /// </summary>
        /// <param name="sessionFactory"></param>
        /// <param name="session"></param>

        public ManagerBase(ISessionFactory sessionFactory, ISession session)
        {
            this.SessionFactory = sessionFactory;
            this._session = new LazyDisposable<ISession>(() => session);
            this._ownsSession = false;
        }


        public void Dispose()
        {
            Dispose(true);
        }

        ~ManagerBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this._ownsSession) {
                this._session.Dispose();
            }
        }
    }
}
