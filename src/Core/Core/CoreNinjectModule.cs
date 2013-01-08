﻿using Dolstagis.Core.Mail;
using Dolstagis.Core.Time;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dolstagis.Core
{
    public class CoreNinjectModule : NinjectModule
    {
        public string ConnectionString { get; private set; }

        public CoreNinjectModule(string connectionString)
        {
            string[] keys = new string[] {
                connectionString,
                connectionString + "." + Environment.MachineName
            };

            this.ConnectionString = keys.Select(x => ConfigurationManager.ConnectionStrings[x])
                .Where(x => x != null)
                .Select(x => x.ConnectionString)
                .FirstOrDefault(x => x != null)
                ?? connectionString;
        }


        private ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(this.ConnectionString)
                    .FormatSql().DoNot.ShowSql()
                )
                .Mappings(x => x.FluentMappings.AddFromAssembly(this.GetType().Assembly))
                .BuildSessionFactory();
        }

        public override void Load()
        {
            Bind<ISessionFactory>().ToMethod(x => BuildSessionFactory()).InSingletonScope();
            Bind<IMailer>().To<SystemNetMailMailer>().InSingletonScope();
            Bind<IClock>().To<SystemClock>().InSingletonScope();
        }
    }
}