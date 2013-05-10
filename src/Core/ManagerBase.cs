using System;
using Dolstagis.Core.Caching;
using Dolstagis.Core.Time;
using NHibernate;
using Ninject;

namespace Dolstagis.Core
{
    public abstract class ManagerBase : IDisposable
    {
        private ICache _cache = new NullCache();
        private IClock _clock = new SystemClock();
        private bool _ownsSession = false;

        protected ISessionFactory SessionFactory { get; private set; }

        protected ISession Session { get; private set; }

        /// <summary>
        ///  Gets or sets the <see cref="ICache"/> instance used for caching.
        /// </summary>

        [Inject, Optional]
        public ICache Cache
        {
            get { return _cache; }
            set { _cache = value; }
        }

        /// <summary>
        ///  Gets or sets the <see cref="IClock"/> instance used to manage the time.
        /// </summary>

        [Inject, Optional]
        public IClock Clock
        {
            get { return _clock; }
            set { _clock = value; }
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
            this.Session = SessionFactory.OpenSession();
            this._ownsSession = true;
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
            this.Session = session;
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
                this.Session.Flush();
                this.Session.Dispose();
            }
        }
    }
}
