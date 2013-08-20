using System;
using System.Linq;
using Dolstagis.Framework.Data;
using NHibernate;
using NHibernate.Linq;

namespace Dolstagis.Framework
{
    public class Manager : IDisposable
    {
        public IRepository Repository { get; private set; }

        public Manager(IRepository repository)
        {
            this.Repository = repository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Repository is IDisposable) ((IDisposable)Repository).Dispose();
        }

        ~Manager()
        {
            Dispose(false);
        }

        /* ====== Common CRUD operations ====== */

        public void Save(object obj)
        {
            this.Repository.Save(obj);
        }
    }

    public class Manager<TRepository> : Manager
        where TRepository : IRepository
    {
        public new TRepository Repository
        {
            get { return (TRepository)base.Repository; }
        }

        public Manager(TRepository repository)
            : base(repository)
        { }
    }
}
