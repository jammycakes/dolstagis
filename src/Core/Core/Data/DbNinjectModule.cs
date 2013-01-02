using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.Data
{
    public class DbNinjectModule : NinjectModule
    {
        private ISessionFactory sessionFactory;

        public DbNinjectModule(string connectionString)
        {
            string[] keys = new string[] {
                connectionString,
                connectionString + "." + Environment.MachineName
            };

            var cs = keys.Select(x => ConfigurationManager.ConnectionStrings[x])
                .Where(x => x != null)
                .Select(x => x.ConnectionString)
                .FirstOrDefault(x => x != null)
                ?? connectionString;

            this.sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(cs)
                    .FormatSql().DoNot.ShowSql()
                )
                .Mappings(x => x.FluentMappings.AddFromAssembly(this.GetType().Assembly))
                .BuildSessionFactory();
        }


        public override void Load()
        {
            Rebind<ISession>().ToMethod(x => this.sessionFactory.OpenSession());
        }
    }
}
