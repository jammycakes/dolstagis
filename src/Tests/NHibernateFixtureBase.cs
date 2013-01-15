using Dolstagis.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Ninject;
using NHibernate.Tool.hbm2ddl;

namespace Dolstagis.Tests
{
    public class NHibernateFixtureBase
    {
        protected static IKernel Kernel { get; private set; }

        static NHibernateFixtureBase()
        {
            Kernel = new StandardKernel();
            Kernel.Load(new NHibernateNinjectModule("tests"));
            Kernel.Bind<ISession>().ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession());
        }

        protected ISession Session { get; private set; }

        [TestFixtureSetUp]
        public virtual void CreateSession()
        {
            this.Session = Kernel.Get<ISession>();
            var schema = new SchemaExport(Kernel.Get<NHibernate.Cfg.Configuration>());
            schema.Create(false, true);
        }

        [TestFixtureTearDown]
        public virtual void DisposeSession()
        {
            this.Session.Dispose();
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
