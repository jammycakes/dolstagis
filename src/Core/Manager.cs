using System;
using System.Linq;
using Dolstagis.Core.Data;
using NHibernate;
using NHibernate.Linq;

namespace Dolstagis.Core
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
    }
}
