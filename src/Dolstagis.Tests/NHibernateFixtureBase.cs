using System.Data;
using Dolstagis.Framework.Data;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using NUnit.Framework;

namespace Dolstagis.Tests
{
    public class NHibernateFixtureBase
    {
        /// <summary>
        ///  Service locator.
        /// </summary>

        protected IKernel Kernel { get; private set; }

        /// <summary>
        ///  The NHibernate session.
        /// </summary>

        protected ISession Session { get; private set; }

        /// <summary>
        ///  The database connection.
        /// </summary>

        protected IDbConnection Connection { get; private set; }

        /// <summary>
        ///  Called before the NHibernate session factory has been bound to load the module(s)
        ///  under test.
        /// </summary>

        protected virtual void LoadModules()
        {
        }

        /// <summary>
        ///  Called before the tests in the fixture are run.
        /// </summary>
        /// <remarks>
        ///  Implementors should override this method rather than create a new one
        ///  with the [TestFixtureSetUp] attribute, otherwise the new methods are
        ///  not guaranteed to run after the test mappings and database connection
        ///  have been initialised.
        /// </remarks>

        protected virtual void BeforeFixture()
        {
        }

        /// <summary>
        ///  Called after the tests in the fixture have been run.
        /// </summary>
        /// <remarks>
        ///  Implementors should override this method rather than create a new one
        ///  with the [TestFixtureTearDown] attribute, otherwise the new methods are
        ///  not guaranteed to run before the test database connection and services
        ///  have been disposed.
        /// </remarks>

        protected virtual void AfterFixture()
        {
        }

        /// <summary>
        ///  Called immediately before each test in the fixture is run.
        /// </summary>
        /// <remarks>
        ///  Implementors should override this method rather than create a new one
        ///  with the [SetUp] attribute, otherwise the new methods are not guaranteed
        ///  to run after the test session has been cleared.
        /// </remarks>

        protected virtual void BeforeTest()
        {
        }

        /// <summary>
        ///  Called immediately after each test in the fixture is run.
        /// </summary>
        /// <remarks>
        ///  Implementors should override this method rather than create a new one
        ///  with the [TearDown] attribute, otherwise the new methods are not guaranteed
        ///  to run before the test session has been cleared.
        /// </remarks>

        protected virtual void AfterTest()
        {
        }

        /// <summary>
        ///  Sets up the database connection, mappings and Ninject bindings.
        /// </summary>

        [TestFixtureSetUp]
        public void CreateSession()
        {
            Kernel = new StandardKernel();
            Kernel.Load(new NHibernateNinjectModule("tests", true));
            LoadModules();
            Kernel.Bind<ISession>().ToMethod(c => c.Kernel.Get<ISessionFactory>().OpenSession())
                .InSingletonScope();

            this.Session = Kernel.Get<ISession>();
            var schema = new SchemaExport(Kernel.Get<NHibernate.Cfg.Configuration>());
            schema.Execute(true, true, false, this.Session.Connection, null);
            this.Connection = this.Session.Connection;
            this.BeforeFixture();
        }

        /// <summary>
        ///  Disposes of the test database connection.
        /// </summary>

        [TestFixtureTearDown]
        public virtual void DisposeSession()
        {
            this.AfterFixture();
            this.Session.Close();
            this.Kernel.Dispose();
        }

        [SetUp]
        public virtual void PrepareTest()
        {
            this.Session.Clear();
            this.BeforeTest();
        }

        [TearDown]
        public virtual void CompleteTest()
        {
            this.AfterTest();
            this.Session.Clear();
        }
    }
}
