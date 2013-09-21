using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Dolstagis.Web
{
    public class Application : IDisposable
    {
        private Container container;

        public Application()
        {
            container = new Container();
        }

        public void Init()
        {
            container.Configure(config => {
                config.AddRegistry<DefaultRegistry>();
                foreach (var registry in GetRegistries()) {
                    config.AddRegistry(registry);
                }
            });
        }

        public virtual IEnumerable<Registry> GetRegistries()
        {
            yield break;
        }

        public void ProcessRequest(IRequestContext context)
        {
            using (var nested = container.GetNestedContainer()) {
                nested.GetInstance<IRequestProcessor>().Process(context);
            }
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
