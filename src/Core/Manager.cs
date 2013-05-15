using System.Linq;
using Dolstagis.Core.Data;
using NHibernate;
using NHibernate.Linq;

namespace Dolstagis.Core
{
    public class Manager<TModel> where TModel: new()
    {
        public IRepository<TModel> Repository { get; private set; }

        public Manager(IRepository<TModel> repository)
        {
            this.Repository = repository;
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
