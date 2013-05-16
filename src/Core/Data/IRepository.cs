using System;
using System.Linq;

namespace Dolstagis.Core.Data
{
    public interface IRepository
    {
        void Flush();

        TModel CreatePersistent<TModel>() where TModel: new();

        TModel CreateTransient<TModel>() where TModel: new();

        TModel Get<TModel>(object id);

        void Persist(object obj);

        IQueryable<TModel> Query<TModel>();

        void Save(object obj);

    }

    public interface IRepository<TModel>: IRepository where TModel : new()
    {
        TModel CreatePersistent();

        TModel CreateTransient();

        TModel Get(object id);

        void Persist(TModel obj);

        IQueryable<TModel> Query();

        void Save(TModel obj);
    }
}
