using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Dolstagis.Web
{
    public class Application : IDisposable
    {
        private Lazy<Container> container;

        public Application()
        {
            container = new Lazy<Container>(CreateContainer, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private Container CreateContainer()
        {
            var result = new Container();
            result.Configure(config => {
                config.AddRegistry<DefaultRegistry>();
                foreach (var registry in GetRegistries()) {
                    config.AddRegistry(registry);
                }
            });
            return result;
        }

        public virtual IEnumerable<Registry> GetRegistries()
        {
            yield break;
        }

        public void ProcessRequest(IRequestContext context)
        {
            using (var nested = container.Value.GetNestedContainer()) {
                nested.Configure(x => x.For<IRequestContext>().Use(context));
                nested.GetInstance<IRequestProcessor>().Process(context);
            }
        }

        public void Dispose()
        {
            if (container.IsValueCreated) {
                container.Value.Dispose();
            }
        }
    }
}
