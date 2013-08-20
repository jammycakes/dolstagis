using System;
using System.Linq;
using Dolstagis.Framework.Data;
using NHibernate;
using NHibernate.Linq;

namespace Dolstagis.Framework
{
    public class TypedManager<TModel> : Manager where TModel: new()
    {
        public new IRepository<TModel> Repository
        {
            get { return ((IRepository<TModel>)base.Repository); }
        }

        public TypedManager(IRepository<TModel> repository)
            : base(repository)
        { }


        /* ====== Common CRUD operations ====== */

        public TModel Get(object id)
        {
            return this.Repository.Get(id);
        }

        public TModel CreateTransient()
        {
            return this.Repository.CreateTransient();
        }

        public TModel CreatePersistent()
        {
            return this.Repository.CreatePersistent();
        }

        public void Save(TModel obj)
        {
            this.Repository.Save(obj);
        }

        public void Delete(TModel obj)
        {
            this.Repository.Delete(obj);
        }
    }


    public class TypedManager<TModel, TRepository> : TypedManager<TModel>
        where TModel : new()
        where TRepository : IRepository<TModel>
    {
        public new TRepository Repository
        {
            get { return (TRepository)base.Repository; }
        }

        public TypedManager(TRepository repository)
            : base(repository)
        { }
    }
}
