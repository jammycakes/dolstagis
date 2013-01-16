using Dolstagis.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Ninject;
using NHibernate.Tool.hbm2ddl;
using Dolstagis.Core;
using System.Data;

namespace Dolstagis.Tests
{
    public class NHibernateFixtureBase
    {
        protected IKernel Kernel { get; private set; }

        protected ISession Session { get; private set; }

        protected IDbConnection Connection { get; private set; }

        [TestFixtureSetUp]
        public virtual void CreateSession()
        {
            Kernel = new StandardKernel();
            Kernel.Load(new NHibernateNinjectModule("tests"));
            Kernel.Bind<ISession>().ToMethod(c => c.Kernel.Get<ISessionFactory>().OpenSession())
                .InSingletonScope();

            this.Session = Kernel.Get<ISession>();
            var schema = new SchemaExport(Kernel.Get<NHibernate.Cfg.Configuration>());
            schema.Execute(true, true, false, this.Session.Connection, null);
            this.Connection = this.Session.Connection;
        }

        [TestFixtureTearDown]
        public virtual void DisposeSession()
        {
            this.Session.Close();
            Kernel.Dispose();
        }

        [SetUp]
        public virtual void BeforeTest()
        {
            this.Session.Clear();
        }

        [TearDown]
        public virtual void AfterTest()
        {
            this.Session.Clear();
        }
    }
}
