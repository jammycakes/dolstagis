using System;
using System.Linq;
using Dolstagis.Core.Data;
using NHibernate;
using NHibernate.Linq;

namespace Dolstagis.Core
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
