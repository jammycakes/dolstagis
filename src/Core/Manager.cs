using System;
using System.Linq;
using Dolstagis.Core.Data;
using NHibernate;
using NHibernate.Linq;

namespace Dolstagis.Core
{
    public class Manager<TModel> : IDisposable where TModel: new()
    {
        public IRepository<TModel> Repository { get; private set; }

        public Manager(IRepository<TModel> repository)
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


    public class Manager<TModel, TRepository> : Manager<TModel>
        where TModel : new()
        where TRepository : IRepository<TModel>
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
