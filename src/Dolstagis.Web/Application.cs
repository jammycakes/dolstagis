﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dolstagis.Web.Http;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Dolstagis.Web
{
    public class Application : IDisposable
    {
        private Container container = new Container();

        public Application()
        {
            container.Configure(config => {
                config.AddRegistry<DefaultRegistry>();
                config.For<Application>().Singleton().Use(this);
            });
        }

        public Application AddModules(params Module[] modules)
        {
            container.Configure(config => {
                foreach (var module in modules) {
                    config.AddRegistry(module.Services);
                    config.For<Module>().Singleton().Add(module);
                }
            });
            return this;
        }

        public IEnumerable<Module> GetModules()
        {
            return this.container.GetAllInstances<Module>();
        }

        public void ProcessRequest(IRequestContext context)
        {
            using (var nested = container.GetNestedContainer()) {
                nested.Configure(x => {
                    x.For<IRequestContext>().Use(context);
                    x.For<IRequest>().Use(context.Request);
                    x.For<IResponse>().Use(context.Response);
                    x.For<IApplicationInfo>().Use(context.Application);
                });
                try {
                    foreach (var instance in nested.GetAllInstances<IRequestProcessor>()) {
                        instance.Process(context);
                    }
                }
                catch (Exception ex) {
                    nested.GetInstance<IExceptionHandler>().HandleException(context, ex);
                }
            }
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
