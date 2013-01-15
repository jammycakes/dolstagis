using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Dolstagis.Core.Data
{
    public class NHibernateNinjectModule : NinjectModule
    {
        public string ConnectionString { get; private set; }

        private Func<FluentConfiguration> configurationProvider;

        public NHibernateNinjectModule(string connectionString)
        {
            string[] keys = new string[] {
                connectionString,
                connectionString + "." + Environment.MachineName
            };

            var cs = keys.Select(x => ConfigurationManager.ConnectionStrings[x])
                .Where(x => x != null)
                .FirstOrDefault(x => x.ConnectionString != null);

            IDictionary<string, Func<FluentConfiguration>> providers =
                new Dictionary<string,Func<FluentConfiguration>>() {
                    { "System.Data.Sqlite", this.ConfigureSqlite },
                    { "System.Data.SqlClient", this.ConfigureMsSql }
                };

            if (cs == null) {
                this.ConnectionString = connectionString;
                this.configurationProvider = this.ConfigureMsSql;
            }
            else {
                this.ConnectionString = cs.ConnectionString;
                if (providers.ContainsKey(cs.ProviderName)) {
                    this.configurationProvider = providers[cs.ProviderName];
                }
                else {
                    this.configurationProvider = this.ConfigureMsSql;
                }
            }
        }


        private FluentConfiguration ConfigureMsSql()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(this.ConnectionString)
                    .FormatSql().DoNot.ShowSql()
                );
        }

        private FluentConfiguration ConfigureSqlite()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                    .ConnectionString(this.ConnectionString)
                    .FormatSql()
                    .DoNot.ShowSql()
                );
        }

        private NHibernate.Cfg.Configuration BuildConfiguration()
        {
            return this.configurationProvider()
                .Mappings(x => x.FluentMappings.AddFromAssembly(this.GetType().Assembly))
                .BuildConfiguration();
        }


        public override void Load()
        {
            Bind<NHibernate.Cfg.Configuration>().ToMethod(x => BuildConfiguration()).InSingletonScope();
            Bind<ISessionFactory>().ToMethod
                (x => x.Kernel.Get<NHibernate.Cfg.Configuration>().BuildSessionFactory())
                .InSingletonScope();
        }
    }
}
