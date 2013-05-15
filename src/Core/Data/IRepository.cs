using System;
using System.Linq;

namespace Dolstagis.Core.Data
{
    public interface IRepository<TModel> where TModel : new()
    {
        TModel CreatePersistent();

        TModel CreateTransient();

        void Flush();

        TModel Get(object id);

        void Persist(TModel obj);

        IQueryable<TModel> Query();

        void Save(TModel obj);
    }
}
